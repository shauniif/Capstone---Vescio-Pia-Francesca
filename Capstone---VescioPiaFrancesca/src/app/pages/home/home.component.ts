import { ArticleService } from './../../Services/article.service';
import { Component, OnInit } from '@angular/core';
import { NationsService } from '../../Services/nations.service';
import { CityService } from '../../Services/city.service';
import { GuildService } from '../../Services/guild.service';
import { EventService } from '../../Services/event.service';
import { EcosService } from '../../Services/ecos.service';
import { iArticle } from '../../interfaces/i-article';
import { iEvent } from '../../interfaces/i-event';
import { AuthService } from '../auth/auth.service';
import { CharacterService } from '../../Services/character.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit  {

  images: { id: number, name: string, src: string }[] = [];
  articles: iArticle[] = [];
  events: iEvent[] = [];
  isLoggedIn:boolean = false;
  constructor(private nationSvc: NationsService, private citySvc: CityService, private guildSvc: GuildService, private eventSvc: EventService, private ecoSvc: EcosService, private ArticleSvc: ArticleService, private authSvc: AuthService, private characterService: CharacterService) {}


  ngOnInit(): void {
    this.authSvc.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn);
    this.images = [
      {id: 1, name: 'Fallchant', src: '../../../assets/img/1.jpg' },
      {id: 2, name: 'Letviark', src: '../../../assets/img/2.jpg' },
      {id: 3, name: 'Porto di Alkis', src: '../../../assets/img/3.png' }
    ];

    this.ArticleSvc.articles$.subscribe(articles =>{
      this.articles = articles;
    })


    this.eventSvc.events$.subscribe(events =>{
      this.events = events;
    })
  }
}
