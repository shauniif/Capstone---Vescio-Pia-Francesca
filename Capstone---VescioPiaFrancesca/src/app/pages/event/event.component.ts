import { Component, OnInit } from '@angular/core';
import { iEvent } from '../../interfaces/i-event';
import { EventService } from '../../Services/event.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrl: './event.component.scss'
})
export class EventComponent implements OnInit {
  pastEvents: iEvent[] = [];
  currEvents: iEvent[] = [];
  futureEvents: iEvent[] = [];
  constructor(private eventSvc: EventService) {}
  ngOnInit(): void {
    this.eventSvc.events$.subscribe((events) => {

      events.forEach((event, i) => {
        let eventDate = new Date(event.date);
        let today = new Date();

        // setto l'orario a 00:00:00 sia dell'evento sia della data corrente senza considerare le ore
        eventDate.setHours(0, 0, 0, 0)
        today.setHours(0, 0, 0, 0);

        if (eventDate < today) {
          this.pastEvents.push(event);
          console.log("eventi passati:",this.pastEvents)
        } else if (eventDate.getTime() === today.getTime()) {
          this.currEvents.push(event);
          console.log("eventi di oggi:", this.currEvents)
        } else {
          this.futureEvents.push(event);
          console.log("evneti futuri:", this.futureEvents)
        }
      })


    })
  }

}
