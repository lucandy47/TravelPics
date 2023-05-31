import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUserService } from '../services/ui/auth/auth-user.service';
import { UserInfo } from '../services/ui/auth/user-info';

@Component({
  selector: 'travelpics-welcome-page',
  templateUrl: './welcome-page.component.html',
  styleUrls: ['./welcome-page.component.scss']
})
export class WelcomePageComponent implements OnInit{

  imageList!: any[];
  public loggedInUser!: UserInfo;

  constructor(
    private router: Router,
    private authUserService: AuthUserService,
  ) {
    
  }
  ngOnInit(): void {
    this.loggedInUser = this.authUserService.loggedInUser;
    this.imageList = [
      { 
        source: '../../assets/images/banner-1.jpg',
        title:'WELCOME TO TRAVELPICS',
        text: 'Your journey starts here! The love for travelling is gathering us all.' 
      },
      { 
        source: '../../assets/images/banner-2.jpg',
        title:'GLOBAL EXPOSURE',
        text: 'Discover amazing places from all around The Globe.' 
      },
      { 
        source: '../../assets/images/banner-3.jpg',
        title:'SHARED EXPERIENCE',
        text: 'Check other travelers\' experiences in your desired destinations.' 
      },
    ];
  }

  redirect(): void{
    if(!!this.loggedInUser){
      this.router.navigate(['navigation/home']);
    }else{
      this.router.navigate(['user/register'])
    }
  }
}
