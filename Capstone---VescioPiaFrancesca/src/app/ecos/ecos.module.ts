import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EcosRoutingModule } from './ecos-routing.module';
import { EcosComponent } from './ecos.component';
import { SharedModule } from '../shared/shared.module';
import { DetailComponent } from './detail/detail.component';


@NgModule({
  declarations: [
    EcosComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    EcosRoutingModule,
    SharedModule
  ]
})
export class EcosModule { }
