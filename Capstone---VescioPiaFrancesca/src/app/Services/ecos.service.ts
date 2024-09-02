import { iEco } from './../interfaces/i-eco';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EcosService {

  constructor(private http:HttpClient) { }

  nationUrl:string = `${environment.apiUrl}EcoApi`


  getAll(): Observable<iEco[]> {
  return this.http.get<iEco[]>(this.nationUrl);
  }

  getEco(id: number) : Observable<iEco>  {
    return this.http.get<iEco>(`${this.nationUrl}/${id}`);
  }


}
