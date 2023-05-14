import { Injectable } from '@angular/core';
import { AuthUserService } from '../auth/auth-user.service';
import { NotificationService } from '../../api/notification.service';
import { InAppNotification } from '../../api/dtos/notification';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InAppNotificationsService {

  constructor(
    private _notificationService: NotificationService
  ) { }

  private intervalTimer: any;

  private notifications$: BehaviorSubject<InAppNotification[]> = new BehaviorSubject<InAppNotification[]>([]);
  public notifications = this.notifications$.asObservable();
  
  private getLoggedInUserNotifications(loggedInUserId: number): void{
    if(loggedInUserId > 0){
      this._notificationService.getLoggedInNotifications(loggedInUserId).subscribe({
        next: (notifs: InAppNotification[]) => {
          console.log(notifs);
          this.notifications$.next(notifs);
        },
        error: (error: any) =>{
          console.log(error);
        }
      });
    }
  }

  public startNotificationTimer(loggedInUserId: number): void {
    console.log("Start notifications.");
    this.getLoggedInUserNotifications(loggedInUserId);
    this.intervalTimer = setInterval(() => {
      this.getLoggedInUserNotifications(loggedInUserId);
    }, 10000); 
  }

  public stopNotificationTimer(): void {
    console.log("Stop notifications.");
    clearInterval(this.intervalTimer);
  }
}
