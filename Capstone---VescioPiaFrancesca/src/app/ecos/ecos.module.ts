import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EcosRoutingModule } from './ecos-routing.module';
import { EcosComponent } from './ecos.component';


@NgModule({
  declarations: [
    EcosComponent
  ],
  imports: [
    CommonModule,
    EcosRoutingModule
  ]
})
export class EcosModule { }
