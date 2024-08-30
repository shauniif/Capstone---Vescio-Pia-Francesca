import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NationsRoutingModule } from './nations-routing.module';
import { NationsComponent } from './nations.component';
import { SharedModule } from '../shared/shared.module';
import { DetailComponent } from './detail/detail.component';

@NgModule({
  declarations: [
    NationsComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    NationsRoutingModule,
    SharedModule
  ]
})
export class NationsModule { }
