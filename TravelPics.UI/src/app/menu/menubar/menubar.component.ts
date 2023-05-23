import { Component, OnInit, ViewChild } from '@angular/core';
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

@Component({
  selector: 'travelpics-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.scss']
})
export class MenubarComponent implements OnInit  {
  @ViewChild('notificationPanel') notificationPanel!: OverlayPanel;

  constructor(
    private _authUserService: AuthUserService,
    private router: Router,
    private _inAppNotificationService: InAppNotificationsService,
    private _notificationService: NotificationService,
    public imageHelperService: ImageService,
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

  ngOnInit(): void {
    this.getLoggedInUser();
    this.getNewNotifications();
  }

  private getNewNotifications(): void{
    this._inAppNotificationService.notifications.subscribe({
      next: (notifs: InAppNotification[]) =>{
        this.newNotifications = notifs;
        let receivedNotifs = this.newNotifications.filter(nn => nn.notificationLog.status == NotificationStatusEnum.Received);
        this.items[0].badge = receivedNotifs.length.toString();
      }
    });
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
        this.items = [
          {
            label: "Notifications",
            icon: 'pi pi-bell',
            visible: this.isUserLoggedIn,
            routerLink: '',
            command: () => {
              this.showNotificationPanel();
            }
          },
          {
            label: "Map",
            icon: 'pi pi-map',
            routerLink: ''
          },
        ];
      },
      error: ()=>{

      }
    });
  }

  public showNotificationPanel(): void {
    if (this.notificationPanel) {
      this.items[0].badge = "0";
      this.notificationPanel.toggle(event);
    }
  }

  public goToLogin(): void{
    this.router.navigate(['auth/login']);
  }

  public readNotifications(): void{
    console.log(this.isReadRequired());
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

}
