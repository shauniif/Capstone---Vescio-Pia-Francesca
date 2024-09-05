import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CharacterComponent } from './character.component';
import { CreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: '',
    component: CharacterComponent
  },
  {
    path:'create',
    component: CreateComponent
  },
  {
    path: 'edit/:id',
    component: CreateComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CharacterRoutingModule { }
