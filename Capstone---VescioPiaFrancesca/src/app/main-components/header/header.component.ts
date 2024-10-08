import { iUser } from './../../interfaces/i-user';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../pages/auth/auth.service';
import { Route, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  isLoggedIn:boolean = false;
  searchForm!: FormGroup;
  isCollapsed:boolean = true;
  showD: boolean = false;
  showDProfile: boolean = false;

  user!: iUser | null;
  constructor(private authSvc:AuthService, private router: Router, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.authSvc.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn);
    this.authSvc.user$.subscribe((user) => {
      this.user = user
    }
    );
    this.searchForm = this.fb.group({
      search: ['']
    })
  }


  toggleDropDown() {
    this.showD = !this.showD;


  }

  logout() : void
  {
   this.authSvc.logout();
  }


  Search(): void {
    let search = this.searchForm.get('search')?.value;

    this.router.navigate(['search/', search]);
  }
}
