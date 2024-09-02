import { ICommentCreate } from './../../interfaces/i-comment-create';
import { Component, OnInit } from '@angular/core';
import { iArticle } from '../../interfaces/i-article';
import { ArticleService } from '../../Services/article.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { iComment } from '../../interfaces/i-comment';
import { CommentService } from '../../Services/comment.service';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit {

  article!: iArticle
  commentForm!: FormGroup
  constructor(private articleSvc: ArticleService, private route: ActivatedRoute, private fb: FormBuilder, private commentSvc: CommentService, private authSvc: AuthService) { }
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.articleSvc.getArticle(params.id).subscribe(article => {
        this.article = article;
        console.log("Articolo ricevuto:", this.article)
      })
    })

    this.commentForm = this.fb.group({
      content: this.fb.control(null, [Validators.required]),
 });


  }

  isTouchedInvalid(fieldName:string){
    const field = this.commentForm.get(fieldName);
    return field?.invalid && field?.touched
  }


  CreateComment(): void {
    if (this.commentForm.valid) {
      const newComment: ICommentCreate = this.commentForm.value;
      this.authSvc.authSubject.subscribe(authSubject => {
        if (authSubject && authSubject.id)
          {
            newComment.authorId = authSubject.id;
          }
        newComment.articleId = this.article.id;
        this.commentSvc.CreateComment(newComment).subscribe((comment) => {
          console.log('Commento creato con successo:', comment);
          this.article.comments.push(comment);
          this.commentForm.reset();
        })

      });
      console.log(newComment)
    }

  }

  DeleteComment(id:number): void {
    this.commentSvc.DeleteComment(id).subscribe((comment) => {
      console.log('Commento eliminato con successo:', comment);
      this.article.comments = this.article.comments.filter(c => c.id!== id);
    })
  }
}
