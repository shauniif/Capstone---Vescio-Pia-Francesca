import { Component, Input, input } from '@angular/core';
import { iCity } from '../../interfaces/i-city';

@Component({
  selector: 'app-single-city',
  templateUrl: './single-city.component.html',
  styleUrl: './single-city.component.scss'
})
export class SingleCityComponent {
@Input() city!: iCity
}
