import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InAppNotification } from './dtos/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/notifications`;

  constructor(private httpClient: HttpClient) { }

  public getLoggedInNotifications(userId: number): Observable<InAppNotification[]>{
    return this.httpClient.get<InAppNotification[]>(`${this.apiUrl}/${userId}`);
  }

  public readNotifications(userId: number): Observable<string>{
    return this.httpClient.put<string>(`${this.apiUrl}/read/${userId}`,{});
  }
}
