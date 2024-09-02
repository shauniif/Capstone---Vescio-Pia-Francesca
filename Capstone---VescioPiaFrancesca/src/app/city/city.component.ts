import { Component, OnInit } from '@angular/core';
import { iCity } from '../interfaces/i-city';
import { CityService } from '../Services/city.service';
import { RaceService } from '../Services/race.service';
import { iRace } from '../interfaces/i-race';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrl: './city.component.scss'
})
export class CityComponent implements OnInit{
  cities: iCity[] = [];
  races: iRace[] = [];

  constructor(private citySvc: CityService, private raceSvc: RaceService) {}
  ngOnInit(): void {
    this.citySvc.getAll().subscribe
    (cities => {
      this.cities = cities;
      console.log("Città ricevute:", this.cities)
    },
    error => {
      console.error('Errore durante il recupero delle città:', error.message);
      })
  }

  }

