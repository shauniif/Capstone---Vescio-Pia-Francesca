import { ActivatedRoute } from '@angular/router';
import { iCity } from './../../interfaces/i-city';
import { Component, OnInit } from '@angular/core';
import { EcosService } from '../../Services/ecos.service';
import { CityService } from '../../Services/city.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent  implements OnInit{

  city!: iCity;
  constructor(
    private route:ActivatedRoute,
    private citySvc: CityService
  ){}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.citySvc.getCity(params.id).subscribe(city => {
        this.city = city;
      })
    })
  }

}
