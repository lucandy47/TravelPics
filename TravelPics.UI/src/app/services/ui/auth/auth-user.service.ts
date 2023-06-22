import { Injectable } from '@angular/core';
import { Observer, BehaviorSubject, Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { LoginModel } from '../../auth/login-model';
import { UserToken } from '../../auth/token';
import * as moment from 'moment';
import { InAppNotificationsService } from '../notifications/in-app-notifications.service';
import { User } from '../../api/dtos/user';
import { UserService } from '../../api/user.service';
import { ImageService } from '../helpers/image.service';
import { DisplayUserInfo } from '../../api/dtos/display-user-info';

const ACCESS_TOKEN_KEY: string = 'TRAVELPICS-ACCESS-TOKEN';
const EXPIRES_ON_KEY: string = 'TRAVELPICS-EXPIRES-ON';

@Injectable({
  providedIn: 'root'
})
export class AuthUserService {

  private userInfo: UserInfo | null = new UserInfo();
  private userInfo$: BehaviorSubject<UserInfo | null> = new BehaviorSubject<UserInfo | null>(
    null
  );
  private loggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  
  public userLoggedIn = this.userInfo$.asObservable();
  public loggedIn = this.loggedIn$.asObservable();
  private jwtHelper = new JwtHelperService();

  public displayUserInfo$: BehaviorSubject<DisplayUserInfo | undefined> = new BehaviorSubject<DisplayUserInfo | undefined>(undefined);
  public displayUserInfo = this.displayUserInfo$.asObservable();

  constructor(
    private authService: AuthService,
    private router: Router,
    private inAppNotificationService: InAppNotificationsService,
    public imageHelperService: ImageService,
    private userService: UserService,
  ) { }

  logout(): void {
    this.userInfo = null;
    this.userInfo$.next(this.userInfo);
    this.loggedIn$.next(false);
    this.displayUserInfo$.next(undefined);
    this.clearAuthFromLocalStorage();
    this.router.navigate(['auth/login']);
    this.inAppNotificationService.stopNotificationTimer();
  
    setTimeout(() => {
      window.location.reload();
    }, 100);
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
    this.userInfo.userId = decodedToken.Id;
    this.userInfo.email = decodedToken.email;
    if(!!this.loggedInUser && !!this.loggedInUser.userId){
      this.userService.getUserInfo(this.loggedInUser.userId).subscribe({
        next: (user: User) =>{
          if(!!user.profileImage){
            this.userInfo!.profileImageSrc = this.imageHelperService.getSanitizedBlobUrlFromBase64(user.profileImage.content, user.profileImage.fileName);
            this.displayUserInfo$.next(<DisplayUserInfo>{
              name: this.userInfo!.name,
              profileImageSrc: this.userInfo!.profileImageSrc
            });
          }
        },
        error: (error: any)=>{
        }
      });
    }


    this.loggedIn$.next(true);
    this.userInfo$.next(this.userInfo);

    if(this.userInfo.userId > 0){
      this.inAppNotificationService.startNotificationTimer(this.userInfo.userId);
    }
  }

  private setAuthToLocalStorage(authorization: UserToken): void {
    window.localStorage.setItem(ACCESS_TOKEN_KEY, authorization.accessToken);
    window.localStorage.setItem(
      EXPIRES_ON_KEY,
      authorization.expiresOn.toString()
    );
  }

  public handleLoggedInUser(): void {
    this.router.navigateByUrl('navigation/home');
  }

  get loggedUserName(): string {
    return !!this.userInfo ? this.userInfo.name : '';
  }

  get loggedInUser(): UserInfo{
    return this.userInfo ?? new UserInfo();
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

  public tryResoreUserSession(): void | Promise<boolean>{
    if(
      !window.localStorage.getItem(ACCESS_TOKEN_KEY) || 
      !window.localStorage.getItem(EXPIRES_ON_KEY)
    ){
      return;
    }
    const authorization = <UserToken>{
      accessToken: window.localStorage.getItem(ACCESS_TOKEN_KEY),
      expiresOn: moment.parseZone(window.localStorage.getItem(EXPIRES_ON_KEY)),
    };
    this.inAppNotificationService.stopNotificationTimer();
    this.setUserSession(authorization);
  }
}
