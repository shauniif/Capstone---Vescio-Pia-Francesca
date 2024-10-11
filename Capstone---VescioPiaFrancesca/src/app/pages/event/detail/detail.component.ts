import { Component, OnInit } from '@angular/core';
import { iEvent } from '../../../interfaces/i-event';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../../Services/event.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit {
  event!: iEvent | undefined;
  formattedDate!: string;
  constructor(
    private route:ActivatedRoute,
    private eventSvc: EventService,

  ){}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {;
      this.eventSvc.events$.subscribe(events => {
        this.event = events.find(event => event.id == params.id);

      })
    })
  }




}
