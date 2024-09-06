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
  constructor(private authSvc:AuthService, private router: Router, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.authSvc.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn);

    this.searchForm = this.fb.group({
      search: ['']
    })
  }


  logout() : void
  {
   this.authSvc.logout();
  }


  Prova(): void {
    let search = this.searchForm.get('search')?.value;
    console.log(search);
    this.router.navigate(['search/', search]);
  }
}
