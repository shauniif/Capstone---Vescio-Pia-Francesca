import { Component, OnInit } from '@angular/core';
import { iEvent } from '../../interfaces/i-event';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../Services/event.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit {

  event!: iEvent;
  constructor(
    private route:ActivatedRoute,
    private eventSvc: EventService,
  ){}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {;
      this.eventSvc.getEvent(params.id).subscribe(event => {
        this.event = event;
      })
    })
  }
}
