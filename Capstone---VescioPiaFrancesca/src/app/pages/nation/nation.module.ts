import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NationRoutingModule } from './nation-routing.module';
import { NationComponent } from './nation.component';
import { DetailComponent } from './detail/detail.component';
import { SharedModule } from '../../shared/shared.module';


@NgModule({
  declarations: [
    NationComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    NationRoutingModule,
    SharedModule
  ]
})
export class NationModule { }
