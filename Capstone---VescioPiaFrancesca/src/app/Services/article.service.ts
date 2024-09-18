import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iArticle } from '../interfaces/i-article';
import { BehaviorSubject, Observable } from 'rxjs';
import { iUser } from '../interfaces/i-user';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  article: iArticle[] = [];
  private articlesSubject = new BehaviorSubject<iArticle[]>([]);
  articles$ = this.articlesSubject.asObservable();
  constructor(private http: HttpClient) {
    this.getAll();
  }
  articleApi: string = `${environment.apiUrl}ArticleApi`


  getAll()
  {
    return this.http.get<iArticle[]>(this.articleApi).subscribe((data) => {
      this.article = data;
      this.articlesSubject.next(this.article);
    });
   }

  getArticle(id: number): Observable<iArticle> {
    return this.http.get<iArticle>(`${this.articleApi}/${id}`);
  }

  GetAuthor(id: number): Observable<iUser> {
    return this.http.get<iUser>(`${this.articleApi}/author/${id}`);
  }
}
