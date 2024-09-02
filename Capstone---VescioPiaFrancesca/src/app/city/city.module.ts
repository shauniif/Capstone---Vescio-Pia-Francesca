import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CityRoutingModule } from './city-routing.module';
import { CityComponent } from './city.component';
import { SharedModule } from '../shared/shared.module';
import { DetailComponent } from './detail/detail.component';


@NgModule({
  declarations: [
    CityComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    CityRoutingModule,
    SharedModule
  ]
})
export class CityModule { }
