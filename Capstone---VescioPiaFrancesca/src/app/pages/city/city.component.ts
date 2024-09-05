import { Component, OnInit } from '@angular/core';
import { CityService } from '../../Services/city.service';
import { iCity } from '../../interfaces/i-city';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrl: './city.component.scss'
})
export class CityComponent implements OnInit {
  cities: iCity[] = [];
  constructor(private citySvc: CityService) {}
  ngOnInit(): void {
    this.citySvc.cities$.subscribe((cities) =>{
      this.cities = cities;
    })
  }
}
