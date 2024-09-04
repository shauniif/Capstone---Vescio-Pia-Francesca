import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../pages/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  isLoggedIn:boolean = false;

  constructor(private authSvc:AuthService) {}
  ngOnInit(): void {
    this.authSvc.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn);
  }


  logout() : void
  {
   this.authSvc.logout();
  }


}
