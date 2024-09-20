import { ArticleComponent } from './../article.component';
import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../../Services/article.service';
import { ActivatedRoute } from '@angular/router';
import { iUser } from '../../../interfaces/i-user';
import { iArticle } from '../../../interfaces/i-article';

@Component({
  selector: 'app-author-articles',
  templateUrl: './author-articles.component.html',
  styleUrl: './author-articles.component.scss'
})
export class AuthorArticlesComponent implements OnInit  {
  author!: iUser;
  articles: iArticle[] = [];
constructor(
  private articleSvc: ArticleService, private route:ActivatedRoute
) {}
  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      this.articleSvc.GetAuthor(params.id).subscribe(author => {
        if(author) this.author = author;

        this.articleSvc.articles$.subscribe((articles) => {

          this.articles = articles.filter(article => article.author.id === this.author.id);

        })
      });
    })
    }
  }
