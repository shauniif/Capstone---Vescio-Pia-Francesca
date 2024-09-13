import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NationRoutingModule } from './nation-routing.module';
import { NationComponent } from './nation.component';
import { DetailComponent } from './detail/detail.component';
import { SharedModule } from '../../shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    NationComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    NationRoutingModule,
    SharedModule,
    NgbModule
  ]
})
export class NationModule { }
