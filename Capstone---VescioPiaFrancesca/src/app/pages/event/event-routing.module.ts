import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventComponent } from './event.component';
import { DetailComponent } from './detail/detail.component';

const routes: Routes = [
  { path: '',
    component: EventComponent
  },
  {
    path: 'detail/:id',
    component: DetailComponent
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventRoutingModule { }
