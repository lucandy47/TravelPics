import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { StringValidators } from 'src/app/shared/validators/string.validators';
import { LoginModel } from 'src/app/services/auth/login-model';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'travelpics-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService
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
      email: formData.email,
      password: formData.password
    }

    this.authService.login(loginModel).subscribe({
      next: (data: any)=>{
        this.errorMessage = "";
        this.messageService.add({
          severity: 'success',
          summary: 'Login',
          detail: 'Your have been successfully logged in.',
        });
      },
      error: (error: any)=>{
        this.errorMessage = "Incorrect email or password!"
        this.messageService.add({
          severity: 'error',
          summary: 'Login',
          detail: 'Could not log in.',
        });
      }
    })
  }

  public goToRegister(): void{
    this.router.navigate(['user/register']);
  }

}
