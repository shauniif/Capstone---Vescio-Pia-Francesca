import { Component, OnInit } from '@angular/core';
import { EcosService } from '../Services/ecos.service';
import { iEco } from '../interfaces/i-eco';

@Component({
  selector: 'app-ecos',
  templateUrl: './ecos.component.html',
  styleUrl: './ecos.component.scss'
})
export class EcosComponent implements OnInit {

  ecos: iEco[] = [];
  constructor(private ecoSvc: EcosService)
  {
  }
  ngOnInit(): void {
    this.ecoSvc.getAll().subscribe(ecos => {

      this.ecos = ecos
      console.log("Ecos ricevuti:", this.ecos)
    },
    error => {
      console.error('Errore durante il recupero degli echi:', error.message);
    })
  }
}
