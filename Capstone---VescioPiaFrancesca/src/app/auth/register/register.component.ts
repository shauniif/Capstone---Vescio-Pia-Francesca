import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { iUser } from '../../interfaces/i-user';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent  implements OnInit {

  signInForm!:FormGroup;
  constructor(private fb: FormBuilder, private authSvc: AuthService, private router: Router) {}


  ngOnInit()  {

    this.signInForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      username: this.fb.control(null, [Validators.required]),
      email: this.fb.control(null, [Validators.required, Validators.email]),
      dateBirth: this.fb.control(null, [Validators.required]),
      password: this.fb.control(null, [Validators.required]),
    });


  }


  CreateUser(): void {
    if (this.signInForm.valid) {
      const newUser: iUser = this.signInForm.value;
      this.authSvc.register(newUser).subscribe((data) => {
        console.log('User registrato con successo:', data);
        this.router.navigate(['auth/login'])});
    }
  }

  isTouchedInvalid(fieldName:string){
    const field = this.signInForm.get(fieldName);
    return field?.invalid && field?.touched
  }
}


