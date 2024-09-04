import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuildRoutingModule } from './guild-routing.module';
import { GuildComponent } from './guild.component';
import { DetailComponent } from './detail/detail.component';
import { SharedModule } from '../../shared/shared.module';


@NgModule({
  declarations: [
    GuildComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    GuildRoutingModule,
    SharedModule
  ]
})
export class GuildModule { }