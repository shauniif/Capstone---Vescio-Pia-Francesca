import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CityRoutingModule } from './city-routing.module';
import { CityComponent } from './city.component';
import { DetailComponent } from './detail/detail.component';
import { SharedModule } from '../../shared/shared.module';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    CityComponent,
    DetailComponent,
  ],
  imports: [
    CommonModule,
    CityRoutingModule,
    SharedModule,
    NgbCollapse
  ]
})
export class CityModule { }
