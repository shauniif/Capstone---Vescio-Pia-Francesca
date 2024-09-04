import { iNations } from './../interfaces/nations';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NationsService {
  constructor(private http:HttpClient) { }

  private nationsSubject = new BehaviorSubject<iNations[]>([]);
  mations$ = this.nationsSubject.asObservable();
  nationUrl:string = `${environment.apiUrl}NationApi`

  getAll() {
  return this.http
    .get<iNations[]>(this.nationUrl)
    }

  getNation(id: number) {
    return this.http
    .get<iNations>(`${this.nationUrl}/${id}`)
  }
}

