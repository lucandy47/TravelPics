import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from './dtos/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/users`;

  constructor(private httpClient: HttpClient) { }

  public registerUser(user: User): Observable<string> {
    return this.httpClient.post<string>(`${this.apiUrl}`, user);
  }
}
