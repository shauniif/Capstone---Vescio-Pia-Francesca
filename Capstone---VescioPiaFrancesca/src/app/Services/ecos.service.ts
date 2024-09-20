import { iEco } from './../interfaces/i-eco';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EcosService {

  constructor(private http:HttpClient) {
    this.getAll();
   }

  ecos: iEco[] = [];
  private ecosSubject = new BehaviorSubject<iEco[]>([]);
  ecos$ = this.ecosSubject.asObservable();
  ecoUrl:string = `${environment.apiUrl}EcoApi`


  getAll() {
  return this.http.get<iEco[]>(this.ecoUrl)
  .subscribe((data) => {
      this.ecos = data;
      this.ecosSubject.next(this.ecos);
    });
  }

  getEco(id: number) : Observable<iEco>  {

    return this.http.get<iEco>(`${this.ecoUrl}/${id}`);
  }


}
