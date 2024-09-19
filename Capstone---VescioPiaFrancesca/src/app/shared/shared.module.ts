import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SingleNationComponent } from './single-nation/single-nation.component';
import { SingleEcoComponent } from './single-eco/single-eco.component';
import { SingleCityComponent } from './single-city/single-city.component';
import { SingleGuildComponent } from './single-guild/single-guild.component';
import { SingleEventComponent } from './single-event/single-event.component';
import { SingleArticleComponent } from './single-article/single-article.component';
import { CoomingSoonComponent } from './cooming-soon/cooming-soon.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    SingleNationComponent,
    SingleEcoComponent,
    SingleCityComponent,
    SingleGuildComponent,
    SingleEventComponent,
    SingleArticleComponent,
    CoomingSoonComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
  ],
  exports:
  [
    SingleNationComponent,
    SingleEcoComponent,
    SingleCityComponent,
    SingleGuildComponent,
    SingleEventComponent,
    SingleArticleComponent,
    CoomingSoonComponent
  ]
})
export class SharedModule { }
