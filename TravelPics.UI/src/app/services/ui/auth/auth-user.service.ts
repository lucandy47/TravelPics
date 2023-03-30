import { Injectable } from '@angular/core';
import { Observer, BehaviorSubject, Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { LoginModel } from '../../auth/login-model';
import { UserToken } from '../../auth/token';
import * as moment from 'moment';

const ACCESS_TOKEN_KEY: string = 'TRAVELPICS-ACCESS-TOKEN';
const EXPIRES_ON_KEY: string = 'TRAVELPICS-EXPIRES-ON';

@Injectable({
  providedIn: 'root'
})
export class AuthUserService {

  private userInfo: UserInfo | null = new UserInfo();
  private loggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  public loggedIn = this.loggedIn$.asObservable();
  private jwtHelper = new JwtHelperService();

  constructor(
    private authService: AuthService,
    private router: Router,
  ) { }

  logout(): void{
    this.userInfo = null;
    this.loggedIn$.next(false);
    this.clearAuthFromLocalStorage();
    this.router.navigate(['auth/login']);
  }

  public login(loginModel: LoginModel): Observable<boolean> {
    return new Observable((observer: Observer<boolean>) => {
      let isLoginSuccessful: boolean = false;
      this.authService.login(loginModel).subscribe({
        next: (token: UserToken)=>{
          this.setUserSession(token);
          this.handleLoggedInUser();
          isLoginSuccessful = true;
          observer.next(isLoginSuccessful);
        },
        error:(error:any)=>{
          observer.next(isLoginSuccessful);
        }
      });
    });
  }


  private setUserSession(authorization: UserToken): void {
    if (!authorization.accessToken || !authorization.expiresOn) return;

    this.setAuthToLocalStorage(authorization);
    if (authorization.expiresOn < moment.utc()) {
      return;
    }
    let decodedToken = this.jwtHelper.decodeToken(authorization.accessToken);
    this.userInfo = new UserInfo();
    this.userInfo.authorization = authorization;
    this.userInfo.name = decodedToken.fullName;

    this.loggedIn$.next(true);
  }

  private setAuthToLocalStorage(authorization: UserToken): void {
    window.localStorage.setItem(ACCESS_TOKEN_KEY, authorization.accessToken);
    window.localStorage.setItem(
      EXPIRES_ON_KEY,
      authorization.expiresOn.toString()
    );
  }

  public handleLoggedInUser(): void {
    this.router.navigateByUrl('/home');
  }

  get loggedUserName(): string {
    return !!this.userInfo ? this.userInfo.name : '';
  }

  public getAccessToken(): string | Promise<boolean> {
    if (
      !!this.userInfo &&
      !!this.userInfo.authorization &&
      !!this.userInfo.authorization.accessToken &&
      moment.parseZone(this.userInfo.authorization.expiresOn) > moment.utc()
    ) {
      return this.userInfo.authorization.accessToken;
    }
    return '';
  }

  private clearAuthFromLocalStorage(): void {
    window.localStorage.removeItem(ACCESS_TOKEN_KEY);
    window.localStorage.removeItem(EXPIRES_ON_KEY);
  }
}
