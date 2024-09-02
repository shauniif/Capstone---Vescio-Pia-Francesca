import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SingleNationComponent } from './single-nation/single-nation.component';
import { SingleEcoComponent } from './single-eco/single-eco.component';
import { SingleCityComponent } from './single-city/single-city.component';
import { SingleGuildComponent } from './single-guild/single-guild.component';
import { SingleEventComponent } from './single-event/single-event.component';
import { SingleArticleComponent } from './single-article/single-article.component';



@NgModule({
  declarations: [
    SingleNationComponent,
    SingleEcoComponent,
    SingleCityComponent,
    SingleGuildComponent,
    SingleEventComponent,
    SingleArticleComponent
  ],
  imports: [
    CommonModule
  ],
  exports:
  [
    SingleNationComponent,
    SingleEcoComponent,
    SingleCityComponent,
    SingleGuildComponent,
    SingleEventComponent,
    SingleArticleComponent
  ]
})
export class SharedModule { }
