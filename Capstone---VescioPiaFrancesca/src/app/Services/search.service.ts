import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iEco } from '../interfaces/i-eco';
import { iSearchResponse } from '../interfaces/i-search-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  searchUrl:string = `${environment.apiUrl}SearchApi`
  constructor(private http: HttpClient) { }


  GetElementSearched(search:string): Observable<iSearchResponse> {
    return this.http.get<iSearchResponse>(`${this.searchUrl}/${search}`);
  }
}
