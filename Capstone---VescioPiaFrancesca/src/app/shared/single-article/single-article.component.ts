import { Component, Input } from '@angular/core';
import { iArticle } from '../../interfaces/i-article';

@Component({
  selector: 'app-single-article',
  templateUrl: './single-article.component.html',
  styleUrl: './single-article.component.scss'
})
export class SingleArticleComponent {
@Input() article!: iArticle;
}
