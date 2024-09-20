import { Component, OnInit } from '@angular/core';
import { iArticle } from '../../interfaces/i-article';
import { ArticleService } from '../../Services/article.service';
import { iUser } from '../../interfaces/i-user';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss'
})
export class ArticleComponent implements OnInit {


  articles: iArticle[] = [];
  articlesCopy: iArticle[] = [];
  article!: iArticle
  isCollapsed: boolean = true;
  authors: iUser[] = [];
  constructor(private articleSvc: ArticleService) {}
  ngOnInit(): void {
    this.articleSvc.articles$.subscribe((articles) => {
      this.articles = articles;
      this.articlesCopy = [...this.articles];

      this.articles.forEach(article => {
        this.articleSvc.GetAuthor(article.author.id).subscribe(author => {

          if(author.id === article.author.id) {
            article.author = author;
          }
          if(!this.authors.find(a => a.id === author.id)) {
            this.authors.push(author);

          }
        })
      });

      this.article = this.getRandomArticle();
    });
  }


  getRandomArticle(): iArticle {
    if (this.articlesCopy.length === 0) {

      return {} as iArticle;
    }
    const randomIndex = Math.floor(Math.random() * this.articlesCopy.length);
    const [randomArticle] = this.articlesCopy.splice(randomIndex, 1);
    return randomArticle;
  }

  OrderByTitle(): void {
    this.articlesCopy.sort((a, b) => a.title.localeCompare(b.title))
  }

  OrderByDate(): void {
    this.articlesCopy.sort((a, b) => a.publicationDate.localeCompare(b.publicationDate))
  }



  DropDownClose(): void {
      this.isCollapsed = true;
  }

}
