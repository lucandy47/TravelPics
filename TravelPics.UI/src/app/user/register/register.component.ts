import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { StringValidators } from 'src/app/shared/validators/string.validators';
import { User } from 'src/app/services/api/dtos/user';
import { UserService } from 'src/app/services/api/user.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'travelpics-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  constructor(
    private router: Router,
    private userService: UserService,
    private messageService: MessageService
  ){}

  public registerForm!: FormGroup;
  public errorMessage!: string;

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      userId: new FormControl(0, Validators.required),
      email: new FormControl(null, [
        Validators.required,
        StringValidators.emailValidator
      ]),
      password: new FormControl(null, [Validators.required]),
      passwordConfirm: new FormControl(null),
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

    this.errorMessage = "";
  }

  register(): void{
    const formData = this.registerForm.getRawValue();
    const user = <User>{
      userId: formData.userId,
      email: formData.email?.trim(),
      password: formData.password?.trim(),
      firstName: formData.firstName?.trim(),
      lastName: formData.lastName?.trim(),
      phone: formData.phone === '' ? null : formData.phone?.trim(),
      postsCount: null
    };
    this.userService.registerUser(user).subscribe({
      next: (data: any)=>{
        this.messageService.add({
          severity: 'success',
          summary: 'User',
          detail: 'Your have been successfully registered.',
        });
        this.router.navigate(['auth/login']);
      },
      error: (error: any)=>{
        this.messageService.add({
          severity: 'error',
          summary: 'User',
          detail: 'Could not register new user.',
        });
      }
    })
  }

}
