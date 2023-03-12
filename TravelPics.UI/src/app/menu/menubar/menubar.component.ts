import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';

@Component({
  selector: 'travelpics-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.scss']
})
export class MenubarComponent implements OnInit  {
  public items!: MenuItem[];
  public user!: MenuItem[];
  public userIcon!: string;

  public isUserLoggedIn!: boolean;
  constructor(
    private _authUserService: AuthUserService,
  ){}

  ngOnInit(): void {
    this.getLoggedInUser();
  }

  private getLoggedInUser(): void{
    this._authUserService.loggedIn.subscribe({
      next: (isLoggedIn:boolean)=>{
        this.isUserLoggedIn = isLoggedIn;
        this.items = [
          {
            label: "Home",
            visible: this.isUserLoggedIn,
            routerLink: ''
          },
          {
            label: "Notifications",
            icon: 'pi pi-bell',
            visible: this.isUserLoggedIn,
            routerLink: ''
          },
          {
            label: "Map",
            icon: 'pi pi-map',
            routerLink: ''
          },
          {
            label: 'Posts',
            icon:'pi pi-fw pi-user',
            visible: this.isUserLoggedIn,
            items: [
              {
                label: 'My Posts',
                routerLink: '',
              },
              {
                label: 'Friends Posts',
                routerLink: '',
              },
            ],
          }
        ];
      },
      error: ()=>{

      }
    });
  }

  public logout(): void{
    this._authUserService.logout();
  }
  public goToLogin(): void{

  }
}
