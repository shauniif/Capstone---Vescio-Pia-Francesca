import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iEvent } from '../interfaces/i-event';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient)
  { }

  eventUrl:string = `${environment.apiUrl}EventApi`


  getAll() {
    return this.http.get<iEvent[]>(this.eventUrl);
    }

  getEvent(id: number) {
    return this.http.get<iEvent>(`${this.eventUrl}/${id}`)
  }
}
