import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { User } from 'src/app/services/api/dtos/user';
import { UserService } from 'src/app/services/api/user.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';

@Component({
  selector: 'travelpics-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit{

  constructor(
    private _authUserService:AuthUserService,
    private _userService:UserService,
    private messageService: MessageService,
  ){}

  public loggedInUser!: UserInfo;
  public isLoading: boolean = true;

  public user!:User;

  ngOnInit(): void {
    this.loggedInUser = this._authUserService.loggedInUser;
    this.getUserInfo(this.loggedInUser.userId);
  }

  private getUserInfo(userId: number): void{
    this.isLoading = true;
    if(!!userId && userId > 0){
      this._userService.getUserInfo(userId).subscribe({
        next: (user: User) =>{
          this.isLoading = false;
          this.user = user;
        },
        error: (error) =>{
          this.messageService.add({
            severity: 'error',
            summary: 'User',
            detail: 'Could not get user info.',
          });
        }
      });
    }
  }
}
