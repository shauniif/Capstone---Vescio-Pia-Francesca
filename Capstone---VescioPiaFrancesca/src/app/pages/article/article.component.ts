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
  articlesCopy: iArticle[] = [];
  article!: iArticle
  constructor(private articleSvc: ArticleService) {}
  ngOnInit(): void {
    this.articleSvc.articles$.subscribe((articles) => {
      this.articles = articles;
      this.articlesCopy = [...this.articles];
      this.article = this.getRandomArticle();
    });
  }


  getRandomArticle(): iArticle {
    if (this.articles.length === 0) {

      return {} as iArticle;
    }
    const randomIndex = Math.floor(Math.random() * this.articlesCopy.length);
    const [randomArticle] = this.articlesCopy.splice(randomIndex, 1);
    return randomArticle;
  }
}
