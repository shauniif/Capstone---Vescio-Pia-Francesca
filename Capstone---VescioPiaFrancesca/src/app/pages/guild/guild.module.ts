import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuildRoutingModule } from './guild-routing.module';
import { GuildComponent } from './guild.component';
import { DetailComponent } from './detail/detail.component';
import { SharedModule } from '../../shared/shared.module';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';
import { GuildmembersComponent } from './guildmembers/guildmembers.component';


@NgModule({
  declarations: [
    GuildComponent,
    DetailComponent,
    GuildmembersComponent
  ],
  imports: [
    CommonModule,
    GuildRoutingModule,
    SharedModule,
    NgbCollapse
  ]
})
export class GuildModule { }
