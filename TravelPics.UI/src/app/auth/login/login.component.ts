import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { StringValidators } from 'src/app/shared/validators/string.validators';
import { LoginModel } from 'src/app/services/auth/login-model';

@Component({
  selector: 'travelpics-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService
  ){}

  public loginForm!: FormGroup;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [
        Validators.required,
        StringValidators.emailValidator
      ]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  login(): void{
    const formData = this.loginForm.getRawValue();
    const loginModel = <LoginModel>{
      email: formData.email,
      password: formData.password
    }

    this.authService.login(loginModel).subscribe({
      next: (data: any)=>{
        console.log(data);
      },
      error: (error: any)=>{
        console.log(error);
      }
    })
  }

}
