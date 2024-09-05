import { Component } from '@angular/core';
import { NationsService } from '../../Services/nations.service';
import { CityService } from '../../Services/city.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private nationSvc: NationsService, private citySvc: CityService) {}
}
