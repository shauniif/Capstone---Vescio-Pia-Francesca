import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../Services/article.service';
import { iArticle } from '../interfaces/i-article';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss'
})
export class ArticleComponent implements OnInit {

  articles: iArticle[] = [];
  constructor(private articleSvc: ArticleService) {}
  ngOnInit(): void {
    this.articleSvc.getAll().subscribe((articles) => {
      this.articles = articles;
      console.log('Articoli ricevuti:', articles);
    });
  }

}
