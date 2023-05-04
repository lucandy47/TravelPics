import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserCreate } from './dtos/user-create';
import { HttpClient } from '@angular/common/http';
import { User } from './dtos/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/users`;

  constructor(private httpClient: HttpClient) { }

  public registerUser(user: UserCreate): Observable<string> {
    return this.httpClient.post<string>(`${this.apiUrl}`, user);
  }

  public getUserInfo(userId: number): Observable<User>{
    return this.httpClient.get<User>(`${this.apiUrl}/${userId}`);
  }

  public updateUser(formData: FormData): Observable<number>{
    return this.httpClient.put<number>(`${this.apiUrl}`, formData);
  }
}
