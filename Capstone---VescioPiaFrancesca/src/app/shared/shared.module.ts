import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SingleNationComponent } from './single-nation/single-nation.component';



@NgModule({
  declarations: [
    SingleNationComponent
  ],
  imports: [
    CommonModule
  ],
  exports:
  [
    SingleNationComponent
  ]
})
export class SharedModule { }
