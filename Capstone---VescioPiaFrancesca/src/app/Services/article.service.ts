import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iArticle } from '../interfaces/i-article';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) { }

  articleApi: string = `${environment.apiUrl}ArticleApi`


  getAll(): Observable<iArticle[]>
  {
    return this.http.get<iArticle[]>(this.articleApi);
   }

  getArticle(id: number): Observable<iArticle> {
    return this.http.get<iArticle>(`${this.articleApi}/${id}`);
  }

}
