import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { AuthComponent } from './auth.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { GuestGuard } from './guard/guest.guard';
import { AuthGuard } from './guard/auth.guard';

const routes: Routes = [
  { path: '',
    component: AuthComponent
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate:[GuestGuard]
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate:[GuestGuard]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate:[AuthGuard]
  },
  {
    path: 'edit/:id',
    component: RegisterComponent,
    canActivate:[AuthGuard]
  }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
