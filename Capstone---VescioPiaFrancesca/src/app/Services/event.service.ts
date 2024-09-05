import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iEvent } from '../interfaces/i-event';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  events: iEvent[] = [];
  private eventsSubject = new BehaviorSubject<iEvent[]>([]);
  events$ = this.eventsSubject.asObservable();
  constructor(private http: HttpClient)
  {
    this.getAll();
  }

  eventUrl:string = `${environment.apiUrl}EventApi`


  getAll() {
    return this.http.get<iEvent[]>(this.eventUrl).subscribe(data => {
      this.events = data;
      this.eventsSubject.next(this.events);
    });
    }

  getEvent(id: number) {
    return this.http.get<iEvent>(`${this.eventUrl}/${id}`)
  }
}
