import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { PostImage } from 'src/app/services/api/dtos/post-image';
import { User } from 'src/app/services/api/dtos/user';
import { UserService } from 'src/app/services/api/user.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { ImageHelper } from 'src/app/shared/helpers/imageHelper';
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
    private router: Router
  ){}

  public loggedInUser!: UserInfo;
  public isLoading: boolean = true;

  public user!:User;

  public userForm!: FormGroup;
  public selectedFiles: any[] = [];

  public profileImage!: PostImage;

  ngOnInit(): void {
    this.loggedInUser = this._authUserService.loggedInUser;
    this.getUserInfo(this.loggedInUser.userId);

    this.userForm = new FormGroup({
      id: new FormControl(this.loggedInUser.userId, Validators.required),
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
    const formData = new FormData();
    formData.append('Id', this.userForm.controls['id']!.value.toString());
    formData.append('FirstName', this.userForm.controls['firstName']!.value);
    formData.append('LastName', this.userForm.controls['lastName']!.value);
    formData.append('Phone', this.userForm.controls['phone']!.value);
  
    for (let i = 0; i < this.selectedFiles.length; i++) {
      formData.append(`ProfileImage[${i}]`, this.selectedFiles[i]);
    }

    this._userService.updateUser(formData).subscribe({
      next: (data: any) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Update User',
          detail: 'Your information have been successfully saved!',
        });
        this.router.navigate(['/navigation/home']);
      },
      error: (error: any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Update User',
          detail: 'Could not save new information.',
        });
      }
    });
    console.log(formData);
  }

  public async selectFiles(event: any): Promise<void> {
    if(event.currentFiles.length > 0){
      this.selectedFiles = event.currentFiles;
      await this.previewProfileImage();
    }
  }

  private async previewProfileImage(): Promise<void>{
    let images = await ImageHelper.loadImages(this.selectedFiles);
    this.profileImage = images[0];
  }
}
