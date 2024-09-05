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


}
