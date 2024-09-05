import { Injectable } from '@angular/core';
import { iAuthResponse } from '../../interfaces/iauth-response';
import { iAuthData } from '../../interfaces/i-auth-data';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { iUser } from '../../interfaces/i-user';
import { environment } from '../../../environments/environment.development';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authSubject = new BehaviorSubject<null|iUser>(null)
  jwtHelper: JwtHelperService = new JwtHelperService();
  syncisLoggedIn:boolean = false;
  user$ = this.authSubject.asObservable();
  isLoggedIn$ = this.user$.pipe(
    map(user => !!user),
    tap(user => this.syncisLoggedIn = user)
  )

  constructor( private http:HttpClient,  private router: Router) {
    this.restoreUser();
  }

  authUrl: string = `${environment.apiUrl}AuthApi`
  registerUrl: string = `${environment.apiUrl}AuthApi/register`
  loginUrl: string = `${environment.apiUrl}AuthApi/login`
  UploadUrl: string = `${environment.apiUrl}AuthApi/InsertImage`

  register(newUser: Partial<iUser>): Observable<iUser> {
    return this.http.post<iUser>(this.registerUrl, newUser);
  }

  login(AuthData: iAuthData): Observable<iAuthResponse>
  {
   return this.http.post<iAuthResponse>(this.loginUrl, AuthData)
   .pipe(tap(data => {
    this.authSubject.next(data.user);
    localStorage.setItem('accessData', JSON.stringify(data));


    this.autoLogout()
   }))
  }

  logout():void {
    this.authSubject.next(null);
    localStorage.removeItem('accessData');
    this.router.navigate(['auth/login']);
   }

   autoLogout():void {
    const accessData = this.getAccessData();
    if(!accessData) return;
    const expDate= this.jwtHelper.getTokenExpirationDate(accessData.token) as Date
    const expMs = expDate.getTime() - new Date().getTime();
    setTimeout(this.logout, expMs)
   }


   getAccessData():iAuthResponse|null {
    const accessDataJson = localStorage.getItem('accessData');
    if(!accessDataJson) return null
    const accessData:iAuthResponse = JSON.parse(accessDataJson);
    return accessData
   }

   restoreUser(): void {
    const accessData = this.getAccessData();
    if(!accessData) return;
    if(this.jwtHelper.isTokenExpired(accessData.token)) return;
    this.authSubject.next(accessData.user)
    this.autoLogout();
   }

   insertImage(formData: FormData): Observable<iUser>
   {
      return this.http.patch<iUser>(`${this.UploadUrl}`, formData)
   }

   GetUser(id: number): Observable<iUser> {
    return this.http.get<iUser>(`${this.authUrl}/${id}`);
   }

    UpdateUser(id:number,currUser: iUser) : Observable<iUser>{
    return this.http.put<iUser>(`${this.authUrl}/${id}`, currUser)
  }
}
