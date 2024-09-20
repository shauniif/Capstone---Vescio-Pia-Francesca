import { Component, OnInit } from '@angular/core';
import { iEvent } from '../../interfaces/i-event';
import { EventService } from '../../Services/event.service';
import { NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrl: './event.component.scss'
})
export class EventComponent implements OnInit {
  pastEvents: iEvent[] = [];
  currEvents: iEvent[] = [];
  futureEvents: iEvent[] = [];
  showMore = true;
  showMore2 = true;
  initialDisplayCountP: number  = 2;
  initialDisplayCountC: number = 1;
  currentSlideIndex = 0;
  displayedPastEvents: iEvent[] = [];
  displayedCurrEvents: iEvent[] = [];
  constructor(private eventSvc: EventService) {}
  ngOnInit(): void {
    /*
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



    }) */
      this.eventSvc.events$.subscribe((events) => {
        // Dividi gli eventi in base alla loro posizione
        for (let i = 0; i <events.length; i++) {
          if (i < 3) {
            this.pastEvents.push(events[i]);
            this.displayedPastEvents = [... this.pastEvents]
          } else if (i >= 3 && i < 5) {
            this.currEvents.push(events[i]);
            this.displayedCurrEvents = [... this.currEvents]
          } else if (i >= 5 && i < 7) {
            this.futureEvents.push(events[i]);

          }
        }
      });
  }


  toggleShowMore(type: string) {
    if(type == "first") {

      this.showMore = !this.showMore;

    } else if( type == "second"){
      this.showMore2 = !this.showMore2;
    }
    this.updateDisplayedEvents(type);
  }

  updateDisplayedEvents(type: string) {
   if(type == "first") {
    if (this.showMore) {
      this.displayedCurrEvents = this.currEvents;

    } else {
      this.displayedCurrEvents = this.currEvents.slice(0, this.initialDisplayCountC);
    }
  } if(type == "second") {
    if (this.showMore2) {
      this.displayedPastEvents = this.pastEvents;


    } else {
      this.displayedPastEvents = this.pastEvents.slice(0, this.initialDisplayCountP);

    }
  }
}

}


