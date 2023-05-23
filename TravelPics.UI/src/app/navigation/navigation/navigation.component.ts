import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';

@Component({
  selector: 'travelpics-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit{

  public baseRoute: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private _authUserService: AuthUserService,
  ){}

  public isUserLoggedIn!: boolean;

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data)=>{
      this.baseRoute = data.baseRoute;
    });
    this.getLoggedInUser();
  }

  private getLoggedInUser(): void{
    this._authUserService.loggedIn.subscribe({
      next: (isLoggedIn:boolean)=>{
        this.isUserLoggedIn = isLoggedIn;
      },
      error: ()=>{

      }
    });
  }

  public logout(): void{
    this._authUserService.logout();
  }
}
