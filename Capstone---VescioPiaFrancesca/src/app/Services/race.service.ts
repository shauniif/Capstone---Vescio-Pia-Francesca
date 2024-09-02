import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iRace } from '../interfaces/i-race';

@Injectable({
  providedIn: 'root'
})
export class RaceService {

  constructor(private http: HttpClient)
  { }

  raceUrl:string = `${environment.apiUrl}RaceApi`

  getAll() {
  return this.http.get<iRace[]>(this.raceUrl);
  }

  /*
(DA USARE NELLA CREATE DEL PG)
      this.raceSvc.getAll().subscribe
    (races => {
      this.races = races;
      console.log("Razze ricevute:",races)
      console.log("Razze ricevute:",this.races)
    },
    error => {
      console.error('Errore durante il recupero delle città:', error.message);
      })
  */
}
