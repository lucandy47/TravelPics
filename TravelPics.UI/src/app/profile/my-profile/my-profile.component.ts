import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { User } from 'src/app/services/api/dtos/user';
import { UserService } from 'src/app/services/api/user.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { StringValidators } from 'src/app/shared/validators/string.validators';

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

  public userForm!: FormGroup;

  ngOnInit(): void {
    this.loggedInUser = this._authUserService.loggedInUser;
    this.getUserInfo(this.loggedInUser.userId);

    this.userForm = new FormGroup({
      userId: new FormControl(this.loggedInUser.userId, Validators.required),
      email: new FormControl(null, [
        Validators.required,
        StringValidators.emailValidator
      ]),
      firstName: new FormControl (null,[
        Validators.required,
        StringValidators.whiteSpaceValidator
      ]),
      lastName: new FormControl (null,[
        Validators.required,
        StringValidators.whiteSpaceValidator
      ]),
      phone: new FormControl(null)
    });

  }

  private getUserInfo(userId: number): void{
    this.isLoading = true;
    if(!!userId && userId > 0){
      this._userService.getUserInfo(userId).subscribe({
        next: (user: User) =>{
          this.user = user;
          this.isLoading = false;
          this.userForm.patchValue({
            userId: this.user.id,
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            phone: this.user.phone,
            email: this.user.email
          });

          this.userForm.controls['email'].disable();
        },
        error: (error) =>{
          console.log(error);
          this.messageService.add({
            severity: 'error',
            summary: 'User',
            detail: 'Could not get user info.',
          });
        }
      });
    }
  }

  public updateUser(): void{
    
  }
}
