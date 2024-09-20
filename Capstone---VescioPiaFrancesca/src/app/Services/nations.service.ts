import { iNation } from './../interfaces/i-nation';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, catchError, of, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NationsService {
  constructor(private http:HttpClient) {
    this.getAll();
  }

  private nationsSubject = new BehaviorSubject<iNation[]>([]);
  nations$ = this.nationsSubject.asObservable();

  nationUrl:string = `${environment.apiUrl}NationApi`
  nation: iNation[] = []

  getAll(): Subscription {
    return this.http
    .get<iNation[]>(this.nationUrl)
    .subscribe((data) => {
      this.nation = data
      this.nationsSubject.next(this.nation);
    })}
}

