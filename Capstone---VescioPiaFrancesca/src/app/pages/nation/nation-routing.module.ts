import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NationComponent } from './nation.component';
import { DetailComponent } from './detail/detail.component';

const routes: Routes = [
  { path: '',
    component: NationComponent
  },
  { path: 'detail/:id',
    component: DetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NationRoutingModule { }
