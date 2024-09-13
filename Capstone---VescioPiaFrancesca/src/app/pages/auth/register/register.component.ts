import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { iUser } from '../../../interfaces/i-user';
import { AuthService } from '../auth.service';
import { iAuthData } from '../../../interfaces/i-auth-data';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  signInForm!:FormGroup;
  user!: iUser;
  AuthData!: iAuthData
  constructor(private fb: FormBuilder, private authSvc: AuthService, private router: Router, private route: ActivatedRoute) {}


  ngOnInit()  {


    this.signInForm = this.fb.group({
      firstName: this.fb.control(null, [Validators.required]),
      lastName: this.fb.control(null, [Validators.required]),
      username: this.fb.control(null, [Validators.required]),
      email: this.fb.control(null, [Validators.required, Validators.email]),
      dateBirth: this.fb.control(null, [Validators.required]),
      password: this.fb.control(null, [Validators.required]),
    });

    this.route.params.subscribe((params:any) => {
      this.authSvc.GetUser(params.id).subscribe(user => {
        this.user = user;
        let date = new Date(user.dateBirth);


        let day = date.getDate().toString().padStart(2, '0');
        let month = (date.getMonth() + 1).toString().padStart(2, '0');
        let year = date.getFullYear();


        let formattedDate = `${year}-${month}-${day}`;
        this.signInForm.patchValue(
          {
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            username: this.user.username,
            email: this.user.email,
            dateBirth: formattedDate,
            password: this.user.password,
          }
        )
      })
    })

  }


  CreateUser(): void {
    if(this.signInForm.valid && this.user){
      console.log(true);
    } else
    {
      console.log(false);
    }
    if(this.signInForm.valid && this.user) {
      const UserToUpdate: iUser = this.signInForm.value;
      UserToUpdate.id = this.user.id
      this.authSvc.UpdateUser(UserToUpdate.id, UserToUpdate).subscribe(data =>
        {
          this.AuthData = data
          this.authSvc.login(this.AuthData).subscribe(data => console.log(data))
          this.router.navigate(['auth/profile'])
        })
      } else {
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
