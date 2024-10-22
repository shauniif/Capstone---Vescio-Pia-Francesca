import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuildComponent } from './guild.component';
import { DetailComponent } from './detail/detail.component';
import { GuildmembersComponent } from './guildmembers/guildmembers.component';
import { CharacterRoleGuardGuard } from '../character/guard/character-role-guard.guard';

const routes: Routes = [
  {
    path: '',
    component: GuildComponent
  },
  {
    path: 'detail/:id',
    component: DetailComponent
  },
  {
    path: 'members/:id',
    component: GuildmembersComponent,
    canActivate: [CharacterRoleGuardGuard]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GuildRoutingModule { }
