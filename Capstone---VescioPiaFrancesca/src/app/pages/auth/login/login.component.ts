import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Router } from '@angular/router';
import { iAuthData } from '../../../interfaces/i-auth-data';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm!: FormGroup;
  constructor(private fb: FormBuilder, private authSvc: AuthService, private router: Router) {}
  ngOnInit(): void {
  this.loginForm = this.fb.group({
    email: this.fb.control(null, [Validators.required, Validators.email]),
    password: this.fb.control(null, [Validators.required]),
  })
  }

  isTouchedInvalid(fieldName:string){
    const field = this.loginForm.get(fieldName);
    return field?.invalid && field?.touched
  }

  login() : void
  {
    if (this.loginForm.valid)
      {
        let userLogin: iAuthData = this.loginForm.value;
        this.authSvc.login(userLogin).subscribe(data =>
          {
            console.log(data)
        })
      }
  }
}
