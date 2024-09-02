import { Component, Input, OnInit } from '@angular/core';
import { iEco } from '../../interfaces/i-eco';

@Component({
  selector: 'app-single-eco',
  templateUrl: './single-eco.component.html',
  styleUrl: './single-eco.component.scss'
})
export class SingleEcoComponent implements OnInit  {
  @Input() eco!: iEco
  ngOnInit(): void {
    console.log('SingleEcoComponent: ', this.eco)
  }



}
