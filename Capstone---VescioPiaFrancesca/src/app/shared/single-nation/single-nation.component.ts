import { Component, Input } from '@angular/core';
import { iNation } from '../../interfaces/i-nation';

@Component({
  selector: 'app-single-nation',
  templateUrl: './single-nation.component.html',
  styleUrl: './single-nation.component.scss'
})
export class SingleNationComponent {
 @Input() nation!: iNation;
}
