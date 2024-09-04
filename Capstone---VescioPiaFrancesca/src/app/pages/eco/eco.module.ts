import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EcoRoutingModule } from './eco-routing.module';
import { EcoComponent } from './eco.component';
import { SharedModule } from '../../shared/shared.module';
import { DetailComponent } from './detail/detail.component';


@NgModule({
  declarations: [
    EcoComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    EcoRoutingModule,
    SharedModule
  ]
})
export class EcoModule { }
