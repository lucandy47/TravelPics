import { Component, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { Router } from '@angular/router';
import { OverlayPanel } from 'primeng/overlaypanel';
import { InAppNotificationsService } from 'src/app/services/ui/notifications/in-app-notifications.service';
import { InAppNotification } from 'src/app/services/api/dtos/notification';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { NotificationService } from 'src/app/services/api/notification.service';

@Component({
  selector: 'travelpics-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.scss']
})
export class MenubarComponent implements OnInit  {
  @ViewChild('notificationPanel') notificationPanel!: OverlayPanel;

  public items!: MenuItem[];
  public user!: MenuItem[];
  public userIcon!: string;

  public isUserLoggedIn!: boolean;
  constructor(
    private _authUserService: AuthUserService,
    private router: Router,
    private _inAppNotificationService: InAppNotificationsService,
    private _notificationService: NotificationService
  ){}

  public newNotifications: InAppNotification[] = [];
  public loggedInUser!: UserInfo;
  public message: string = "";

  ngOnInit(): void {
    this.getLoggedInUser();
    this.getNewNotifications();
  }

  private getNewNotifications(): void{
    this._inAppNotificationService.notifications.subscribe({
      next: (notifs: InAppNotification[]) =>{
        console.log(notifs);
        this.newNotifications = notifs;
      }
    })
  }

  private getLoggedInUser(): void{
    this.loggedInUser = this._authUserService.loggedInUser;
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

  private showNotificationPanel(): void {
    if (this.notificationPanel) {
      this.notificationPanel.toggle(event);
    }
  }

  public logout(): void{
    this._authUserService.logout();
  }
  public goToLogin(): void{
    this.router.navigate(['auth/login']);
  }

  public readNotifications(): void{
    console.log("read");
    if(!!this.loggedInUser &&  this.loggedInUser.userId > 0){
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
}
