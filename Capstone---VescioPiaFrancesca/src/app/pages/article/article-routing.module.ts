import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleComponent } from './article.component';
import { DetailComponent } from './detail/detail.component';
import { AuthorArticlesComponent } from './author-articles/author-articles.component';

const routes: Routes = [
  { path: '',
    component: ArticleComponent
  },
  {
    path: 'detail/:id',
    component: DetailComponent
  },
  {
    path: 'articlesofanAuthor/:id',
    component: AuthorArticlesComponent,
  }

  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticleRoutingModule { }
