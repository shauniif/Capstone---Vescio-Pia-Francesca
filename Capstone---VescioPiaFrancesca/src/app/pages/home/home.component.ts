import { ArticleService } from './../../Services/article.service';
import { Component } from '@angular/core';
import { NationsService } from '../../Services/nations.service';
import { CityService } from '../../Services/city.service';
import { GuildService } from '../../Services/guild.service';
import { EventService } from '../../Services/event.service';
import { EcosService } from '../../Services/ecos.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private nationSvc: NationsService, private citySvc: CityService, private guildSvc: GuildService, private eventSvc: EventService, private ecoSvc: EcosService, private ArticleSvc: ArticleService) {}
}
