import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iNations } from '../interfaces/nations';
import { iCity } from '../interfaces/i-city';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http:HttpClient) { }


  cityUrl:string = `${environment.apiUrl}CityApi`

  getAll() {
  return this.http.get<iCity[]>(this.cityUrl);
  }

  getCity(id: number) {
    return this.http.get<iCity>(`${this.cityUrl}/${id}`)
  }



}
