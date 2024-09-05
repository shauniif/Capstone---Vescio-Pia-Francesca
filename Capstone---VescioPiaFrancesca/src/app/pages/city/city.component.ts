import { iCity } from './../../interfaces/i-city';
import { Component, OnInit } from '@angular/core';
import { CityService } from '../../Services/city.service';
import { NationsService } from '../../Services/nations.service';
import { filter, map } from 'rxjs';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrl: './city.component.scss'
})
export class CityComponent implements OnInit {
  cities: iCity[] = [];
  nationsName: string[] = [];
  filteredCities: iCity[] = [];

  constructor(private citySvc: CityService, private nationSvc: NationsService ) {}

  ngOnInit(): void {
    this.citySvc.cities$.subscribe((cities) =>{
      this.cities = cities;
      this.filteredCities = [... this.cities]
  })
    this.getNationName();

  }
  getNationName(): void {

    this.nationSvc.nations$
    .pipe(
       map(nations => nations.map(nation => nation.name)
    ))
    .subscribe((nationsname) => {
      this.nationsName = nationsname
    });
  }

  FilterCity(name: string): void {
    this.filteredCities = this.cities.filter(city => city.nation.name === name)
  }

}
