import { Component } from '@angular/core';
import { iNations } from '../../interfaces/nations';
import { NationsService } from '../../Services/nations.service';

@Component({
  selector: 'app-nation',
  templateUrl: './nation.component.html',
  styleUrl: './nation.component.scss'
})
export class NationComponent {

  nations:iNations[] = [];
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
