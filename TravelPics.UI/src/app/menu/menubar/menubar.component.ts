import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { Router } from '@angular/router';
import { OverlayPanel } from 'primeng/overlaypanel';
import { InAppNotificationsService } from 'src/app/services/ui/notifications/in-app-notifications.service';
import { InAppNotification, NotificationStatusEnum } from 'src/app/services/api/dtos/notification';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { NotificationService } from 'src/app/services/api/notification.service';
import { ImageService } from 'src/app/services/ui/helpers/image.service';
import { DisplayUserInfo } from 'src/app/services/api/dtos/display-user-info';
import { AvailableSearchItem } from 'src/app/services/api/dtos/available-search-item';
import { LookupitemsService } from 'src/app/services/api/lookupitems.service';
import { debounceTime } from 'rxjs';
import { EntityTypeEnum } from 'src/app/services/ui/enums/entity-type.enum';

@Component({
  selector: 'travelpics-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.scss']
})
export class MenubarComponent implements OnInit  {
  @ViewChild('notificationPanel') notificationPanel!: OverlayPanel;
  // @ViewChild('menubar', { static: true }) menubar!: ElementRef;

  constructor(
    private _authUserService: AuthUserService,
    private router: Router,
    private _inAppNotificationService: InAppNotificationsService,
    private _notificationService: NotificationService,
    public imageHelperService: ImageService,
    private _lookupItemsService: LookupitemsService,
  ){}

  public items!: MenuItem[];
  public userIcon!: string;

  public isUserLoggedIn!: boolean;

  public newNotifications: InAppNotification[] = [];
  public loggedInUser!: UserInfo;
  public message: string = "";

  public notificationVisible: boolean = false;
  public displayUserInfo!: DisplayUserInfo;

  public loadingDisplay: boolean = true;

  public searchKeyword!: string;

  public results!: AvailableSearchItem[];

  public menuItems!: MenuItem[];
  ngOnInit(): void {
    this.getLoggedInUser();
    this.getNewNotifications();
    this.items[1].badge = "0";
  }

  

  private getNewNotifications(): void{
    this._inAppNotificationService.notifications.subscribe({
      next: (notifs: InAppNotification[]) =>{
        this.newNotifications = notifs;
        let receivedNotifs = this.newNotifications.filter(nn => nn.notificationLog.status == NotificationStatusEnum.Received);
        this.items[1].badge = receivedNotifs.length.toString();
      }
    });
  }

  public goToMainPage(): void{
    this.router.navigate(['']);
  }

  private getLoggedInUser(): void{
    this.loggedInUser = this._authUserService.loggedInUser;
    this._authUserService.displayUserInfo.subscribe({
      next: (userInfoDisplay)=>{
        if(!!userInfoDisplay){
          this.displayUserInfo = userInfoDisplay;
        }
        this.loadingDisplay = false;
      },
      error: (error)=>{
        console.log(error);
      }
    });
    this._authUserService.loggedIn.subscribe({
      next: (isLoggedIn:boolean)=>{
        this.isUserLoggedIn = isLoggedIn;
        this.menuItems = [
          {
            label: "My Profile",
            icon: "pi pi-user",
            routerLink: 'navigation/profile'
          },
          {
            label: "Logout",
            icon: "pi pi-sign-out",
            command: ()=> this._authUserService.logout()
          }
        ]
        this.items = [
          {
            label: "Home",
            icon: 'pi pi-home',
            visible: this.isUserLoggedIn,
            routerLink: 'navigation/home',
          },
          {
            label: "Notifications",
            icon: 'pi pi-bell',
            visible: this.isUserLoggedIn,
            routerLink: '',
            badge: "0",
            command: () => {
              this.showNotificationPanel();
            }
          },
          {
            label: "Map",
            icon: 'pi pi-map',
            routerLink: this.isUserLoggedIn ? 'navigation/map' : 'map'
          },
          {
            label: "Register",
            icon: 'pi pi-user',
            routerLink: 'user/register',
            visible: !this.isUserLoggedIn,

          },
        ];
      },
      error: ()=>{

      }
    });
  }

  public showNotificationPanel(): void {
    if (this.notificationPanel) {
      this.items[1].badge = "0";
      this.notificationPanel.toggle(event);
    }
  }

  public goToLogin(): void{
    this.router.navigate(['auth/login']);
  }

  public readNotifications(): void{
    if(!!this.loggedInUser &&  this.loggedInUser.userId > 0 && this.isReadRequired()){
      this.newNotifications.forEach((notif)=>{
        notif.notificationLog.status = NotificationStatusEnum.Read;
      });
      this._notificationService.readNotifications(this.loggedInUser.userId).subscribe({
        next: (data: string)=>{
          this.message = data;
        },
        error: (error: any) => {
          console.log(error);
        }
      })

    }
  }

  private isReadRequired(): boolean{
    for(let notif of this.newNotifications){
      if(notif.notificationLog.status != NotificationStatusEnum.Read){
        return true;
      }
    }
    return false;
  }

  public isNotificationRead(notification: InAppNotification): boolean{
    return notification.notificationLog.status === NotificationStatusEnum.Read;
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }

  public search(event: any){
    this._lookupItemsService
      .findLookupItems(event.query)
      .pipe(debounceTime(500))
      .subscribe({
        next: (data: AvailableSearchItem[]) =>{
          this.results = data;
        },
        error: (error)=>{
          console.log(error);
        }
      })
  }

  public goToResource(resource: AvailableSearchItem): void{
    switch(resource.entityTypeId){
      case EntityTypeEnum.USER:
        this.router.navigate([`navigation/posts/user/${resource.id}`]);
        break;
      case EntityTypeEnum.LOCATION:
        this.router.navigate(['navigation/posts/location'], { queryParams: { locationName: resource.name } });
        break;
      }
  }
  
  public onAutoCompleteHide(): void{
    this.searchKeyword='';
  }

  public goToPost(postId: number): void{
    this.router.navigate([`navigation/posts/${postId}`]);
    this.notificationPanel.toggle(event);
  }
}
