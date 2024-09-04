import { Component, OnInit } from '@angular/core';
import { iEvent } from '../../interfaces/i-event';
import { EventService } from '../../Services/event.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrl: './event.component.scss'
})
export class EventComponent implements OnInit {

  events: iEvent[] = [];
  constructor(private eventSvc: EventService) {}
  ngOnInit(): void {
    this.eventSvc.getAll().subscribe(events => {

      this.events = events
      console.log("Eventi ricevuti:", this.events)
    },
    error => {
      console.error('Errore durante il recupero degli echi:', error.message);
    })
  }

}
