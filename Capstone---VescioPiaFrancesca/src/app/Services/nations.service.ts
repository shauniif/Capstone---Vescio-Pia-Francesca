import { iNations } from './../interfaces/nations';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NationsService {
  constructor(private http:HttpClient) {
    this.getAll();
  }

  private nationsSubject = new BehaviorSubject<iNations[]>([]);
  nations$ = this.nationsSubject.asObservable();

  nationUrl:string = `${environment.apiUrl}NationApi`
  nation: iNations[] = []


  getNation(id: number) {
    return this.http
    .get<iNations>(`${this.nationUrl}/${id}`)
  }

  getAll() {
    return this.http
    .get<iNations[]>(this.nationUrl)
    .subscribe((data) => {
      this.nation = data
      this.nationsSubject.next(this.nation);
    })}
}

