import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../Services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor( private authSvc: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const accessData = this.authSvc.getAccessData()
    console.log(accessData);
    if(!accessData) return next.handle(request);
    console.log(accessData.token);
    const newRequest = request.clone({
      headers: request.headers.append('Authorization', `Bearer ${accessData.token}`),
    });
    console.log(newRequest.headers);
    return next.handle(newRequest);
  }
}
