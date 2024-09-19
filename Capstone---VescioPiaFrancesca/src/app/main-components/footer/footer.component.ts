import { iUser } from './../../interfaces/i-user';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../pages/auth/auth.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent implements OnInit  {
  isLoggedIn:boolean = false;
constructor(private authSvc: AuthService) {}
  ngOnInit(): void {
    this.authSvc.isLoggedIn$.subscribe(IsLoggedIn => {
      this.isLoggedIn = IsLoggedIn;
    });
  }



}
