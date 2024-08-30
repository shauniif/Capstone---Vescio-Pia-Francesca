import { iNations } from './../interfaces/nations';
import { Component, OnInit } from '@angular/core';
import { NationsService } from '../Services/nations.service';

@Component({
  selector: 'app-nations',
  templateUrl: './nations.component.html',
  styleUrl: './nations.component.scss'
})
export class NationsComponent implements OnInit {

  nations:iNations[] = [];
  constructor(private nationSvc: NationsService) {}
  ngOnInit(): void {
    this.nationSvc.getAll().subscribe
    (nations => {
      this.nations = nations;
      console.log("Nazioni ricevute:", this.nations)
    },
    error => {
      console.error('Errore durante il recupero delle nazioni:', error.message);
    }
  )
  console.log("ciao");
  }


}
