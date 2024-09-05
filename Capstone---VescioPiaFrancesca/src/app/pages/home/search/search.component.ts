import { ActivatedRoute, Route } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnInit  {

  searchQuery!: string;
  constructor( private route: ActivatedRoute) {}
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      console.log(params);
      this.searchQuery = params['search'];
      console.log(this.searchQuery);
    });
  }

}
