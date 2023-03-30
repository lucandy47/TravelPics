import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { StringValidators } from 'src/app/shared/validators/string.validators';
import { LoginModel } from 'src/app/services/auth/login-model';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';

@Component({
  selector: 'travelpics-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authUserService: AuthUserService,
    private router: Router,
  ){}

  public loginForm!: FormGroup;
  public errorMessage!: string;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [
        Validators.required,
        StringValidators.emailValidator
      ]),
      password: new FormControl(null, [Validators.required]),
    });

    this.errorMessage = "";
  }

  login(): void{
    const formData = this.loginForm.getRawValue();
    const loginModel = <LoginModel>{
      email: formData?.email.trim(),
      password: formData?.password
    }

    this.authUserService.login(loginModel).subscribe({
      next: (isLoginSuccessful:boolean)=>{
        if(!isLoginSuccessful) this.errorMessage = "Incorrect email or password!";
      },
      error: ()=>{
        this.errorMessage = "";
      }
    });
  }

  public goToRegister(): void{
    this.router.navigate(['user/register']);
  }

}
