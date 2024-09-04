import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iComment } from '../interfaces/i-comment';
import { Observable } from 'rxjs';
import { ICommentCreate } from '../interfaces/i-comment-create';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http:HttpClient) { }


  commentUrl:string = `${environment.apiUrl}CommentApi`

  CreateComment(newComment: Partial<ICommentCreate>): Observable<iComment> {
    return this.http.post<iComment>(`${this.commentUrl}/Create`, newComment);
  }

  UpdateComment(id:number, newComment: Partial<ICommentCreate>): Observable<iComment> {
    return this.http.put<iComment>(`${this.commentUrl}/${id}`, newComment);
  }
  DeleteComment(id:number) {
    return this.http.delete<iComment>(`${this.commentUrl}/${id}`);
  }
}
