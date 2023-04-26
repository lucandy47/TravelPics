import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthUserService } from '../auth-user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authUserService: AuthUserService
  ){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): 
  Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
  {
    return this.authUserService.loggedIn.pipe(
      map((isLoggedIn: boolean) => {
        if(isLoggedIn){
        }else{
          this.router.navigate(['/auth/login']);
        }
        return isLoggedIn;
      })
    );
  }
  
}
