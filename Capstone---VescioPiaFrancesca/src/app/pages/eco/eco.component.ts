import { Component } from '@angular/core';
import { iEco } from '../../interfaces/i-eco';
import { EcosService } from '../../Services/ecos.service';

@Component({
  selector: 'app-eco',
  templateUrl: './eco.component.html',
  styleUrl: './eco.component.scss'
})
export class EcoComponent {

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
