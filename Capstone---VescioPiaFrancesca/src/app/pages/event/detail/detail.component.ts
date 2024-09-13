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
  event!: iEvent;
  formattedDate!: string;
  constructor(
    private route:ActivatedRoute,
    private eventSvc: EventService,

  ){}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {;
      this.eventSvc.getEvent(params.id).subscribe(event => {
        this.event = event;
        let date = new Date(this.event.date);


      let day = date.getDate().toString().padStart(2, '0');
      let month = (date.getMonth() + 1).toString().padStart(2, '0');
      let year = date.getFullYear();

      this.formattedDate = `${day}-${month}-${year}`;
      })
    })
  }




}
