import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventRoutingModule } from './event-routing.module';
import { EventComponent } from './event.component';
import { SharedModule } from '../../shared/shared.module';
import { DetailComponent } from './detail/detail.component';
import { NgbCarouselModule, NgbCollapse } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    EventComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    EventRoutingModule,
    SharedModule,
    NgbCarouselModule,
    NgbCollapse
  ]
})
export class EventModule { }
