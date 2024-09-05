import { Component, OnInit } from '@angular/core';
import { iArticle } from '../../interfaces/i-article';
import { ArticleService } from '../../Services/article.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss'
})
export class ArticleComponent implements OnInit {


  articles: iArticle[] = [];
  constructor(private articleSvc: ArticleService) {}
  ngOnInit(): void {
    this.articleSvc.articles$.subscribe((articles) => {
      this.articles = articles;
    });
  }

}
