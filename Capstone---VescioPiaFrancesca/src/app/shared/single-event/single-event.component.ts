import { Component, Input } from '@angular/core';
import { iEvent } from '../../interfaces/i-event';

@Component({
  selector: 'app-single-event',
  templateUrl: './single-event.component.html',
  styleUrl: './single-event.component.scss'
})
export class SingleEventComponent {

  @Input() event!: iEvent
}
