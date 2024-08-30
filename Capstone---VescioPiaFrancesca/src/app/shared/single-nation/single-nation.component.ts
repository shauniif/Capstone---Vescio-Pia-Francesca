import { Component, Input } from '@angular/core';
import { iNations } from '../../interfaces/nations';

@Component({
  selector: 'app-single-nation',
  templateUrl: './single-nation.component.html',
  styleUrl: './single-nation.component.scss'
})
export class SingleNationComponent {
 @Input() nation!: iNations;
}
