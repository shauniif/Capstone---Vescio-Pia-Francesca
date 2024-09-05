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
    this.eventSvc.events$.subscribe((events) => {
      this.events = events
    })
  }

}
