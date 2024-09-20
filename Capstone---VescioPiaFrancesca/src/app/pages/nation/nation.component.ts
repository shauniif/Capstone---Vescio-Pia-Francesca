import { Component } from '@angular/core';
import { iNation } from '../../interfaces/i-nation';
import { NationsService } from '../../Services/nations.service';

@Component({
  selector: 'app-nation',
  templateUrl: './nation.component.html',
  styleUrl: './nation.component.scss'
})
export class NationComponent {

  nations:iNation[] = [];
  constructor(private nationSvc: NationsService) {}
  ngOnInit(): void {
    this.nationSvc.nations$.subscribe((nations) =>{
      this.nations = nations;
    })
  }


  OrderNationByName(): void {
    this.nations.sort((a, b) => a.name.localeCompare(b.name))
  }
}
