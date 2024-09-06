import { iSearchResponse } from './../../../interfaces/i-search-response';
import { ActivatedRoute, Route } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SearchService } from '../../../Services/search.service';
import { iGuild } from '../../../interfaces/i-guild';
import { iEco } from '../../../interfaces/i-eco';
import { iCity } from '../../../interfaces/i-city';
import { iNations } from '../../../interfaces/nations';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnInit  {

  searchQuery!: string;
  ecos: iEco[] = [];
  guilds: iGuild[] = [];
  cities: iCity[] = [];
  nations: iNations[] = [];
  constructor( private route: ActivatedRoute, private searchSvc: SearchService) {}
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      console.log(params);
      this.searchQuery = params['search'];
      this.searchSvc.GetElementSearched(this.searchQuery).subscribe((e: iSearchResponse) => {
        console.log(e);
        this.ecos = e.ecos;
        console.log("echi trovati", this.ecos)
        this.guilds = e.guilds;
        console.log("gilde trovate", this.guilds)
        this.cities = e.cities;
        console.log("città trovate", this.cities)
        this.nations = e.nations;
        console.log("nazioni trovate", this.nations)
      })
    });
  }

}
