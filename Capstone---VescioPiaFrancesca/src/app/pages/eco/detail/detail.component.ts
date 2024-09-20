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
      this.ecoSvc.getEco(params.id).subscribe(eco => {
        this.eco = eco;
      })
    })
  }

}
