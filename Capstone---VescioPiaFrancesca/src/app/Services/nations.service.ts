import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iNations } from '../interfaces/nations';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class NationsService {
  constructor(private http:HttpClient) { }


  nationUrl:string = `${environment.apiUrl}NationApi`

  getAll() {
  return this.http.get<iNations[]>(this.nationUrl);
  }

  getNation(id: number) {
    return this.http.get<iNations>(`${this.nationUrl}/${id}`)
  }
}

