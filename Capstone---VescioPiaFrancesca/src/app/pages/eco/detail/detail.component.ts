import { Component } from '@angular/core';
import { iEco } from '../../../interfaces/i-eco';
import { ActivatedRoute } from '@angular/router';
import { EcosService } from '../../../Services/ecos.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {
  eco!: iEco;
  constructor(
    private route:ActivatedRoute,
    private ecoSvc: EcosService
  ){}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      console.log("ID dal parametro della rotta:", params.id);
      this.ecoSvc.getEco(params.id).subscribe(eco => {
        console.log("Eco ricevuto data:", eco);
        this.eco = eco;
        console.log("Eco ricevuto:", this.eco)
      })
    })
  }

}
