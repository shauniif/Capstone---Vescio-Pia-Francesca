
import { Component, OnInit } from '@angular/core';
import { iArticle } from '../../../interfaces/i-article';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { iComment } from '../../../interfaces/i-comment';
import { ArticleService } from '../../../Services/article.service';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from '../../../Services/comment.service';

import { ICommentCreate } from '../../../interfaces/i-comment-create';
import { AuthService } from '../../auth/auth.service';
import { iUser } from '../../../interfaces/i-user';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit {
  article!: iArticle
  createCommentForm!: FormGroup;
  updateCommentForm!: FormGroup;
  currentUserLog!: iUser | null;
  currComment!: iComment | null;

  constructor(private articleSvc: ArticleService, private route: ActivatedRoute, private fb: FormBuilder, private commentSvc: CommentService, private authSvc: AuthService) {
    this.authSvc.authSubject.subscribe(authSubject => {
      if (authSubject && authSubject.id)
        {
          this.currentUserLog = authSubject
        }
      })
  }
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.articleSvc.getArticle(params.id).subscribe(article => {
        this.article = article;
        this.articleSvc.GetAuthor(article.author.id).subscribe(author =>
          {
            console.log(author)
            this.article.author = author;
          })
        this.article.comments.forEach(comment => {
          this.articleSvc.GetAuthor(comment.author.id).subscribe(author =>
            {
              comment.author = author;
            })
        })
      })
    })

    this.createCommentForm = this.fb.group({
      content: this.fb.control(null, [Validators.required]),

 });

 this.updateCommentForm = this.fb.group({
  content: this.fb.control(null, [Validators.required]),

});
  }

  PrepareToUpdate(id: number): void {
    let comment = this.article.comments.find(c => c.id == id)
    if(comment == null) return
    this.currComment = comment
    this.updateCommentForm.patchValue({
      content: comment?.content
    });
  }



  isTouchedInvalid(form: FormGroup, fieldName:string){
    const field = form.get(fieldName);
    return field?.invalid && field?.touched
  }



  CreateComment(): void {
    if (this.createCommentForm.valid) {
      const newComment: ICommentCreate = this.createCommentForm.value;
      if(this.currentUserLog) {
        newComment.authorId = this.currentUserLog.id;
      }

        newComment.articleId = this.article.id;
        this.commentSvc.CreateComment(newComment).subscribe((comment) => {
          this.articleSvc.GetAuthor(comment.author.id).forEach((author) => {
            comment.author = author;
          })
          this.article.comments.push(comment);
          this.createCommentForm.reset();
        })

        console.log(newComment)
      };
    }


  UpdateComment(): void {
    if (this.updateCommentForm.valid) {
      console.log(this.updateCommentForm.value)
      const updatedComment: ICommentCreate = this.updateCommentForm.value;
      this.authSvc.authSubject.subscribe(authSubject => {
        if (authSubject && authSubject.id)
          {
            updatedComment.authorId = authSubject.id;
          }
          updatedComment.articleId = this.article.id;
      if(this.currComment == null) return
      this.commentSvc.UpdateComment(this.currComment?.id, updatedComment).subscribe((comment) => {
        this.articleSvc.GetAuthor(comment.author.id).forEach((author) => {
          comment.author = author;
        })
        this.article.comments = this.article.comments.map(c => c.id === comment.id? comment : c);
        this.currComment = null;
        this.createCommentForm.reset();
      })
    })}
  }

  DeleteComment(id:number): void {
    this.commentSvc.DeleteComment(id).subscribe((comment) => {
      console.log('Commento eliminato con successo:', comment);
      this.article.comments = this.article.comments.filter(c => c.id!== id);
    })
  }
}

