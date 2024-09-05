import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iNations } from '../interfaces/nations';
import { iCity } from '../interfaces/i-city';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  cities: iCity[] = [];
  private citiesSubject = new BehaviorSubject<iCity[]>([]);
  cities$ = this.citiesSubject.asObservable();
  constructor(private http:HttpClient) {
    this.getAll();
  }


  cityUrl:string = `${environment.apiUrl}CityApi`


  getCity(id: number) {
    return this.http.get<iCity>(`${this.cityUrl}/${id}`)
  }

  getAll() {
    return this.http
    .get<iCity[]>(this.cityUrl)
    .subscribe((data) => {
      this.cities = data
      this.citiesSubject.next(this.cities);
    })}



}
