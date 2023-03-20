import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserToken } from './token';
import { LoginModel } from './login-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly authApiUrl = `${environment.baseDashboardApiUrl}/api/auth`;

  constructor(private httpClient: HttpClient) {}

  public login(loginModel: LoginModel): Observable<UserToken> {
    return this.httpClient.put<UserToken>(`${this.authApiUrl}`, loginModel);
  }
}
