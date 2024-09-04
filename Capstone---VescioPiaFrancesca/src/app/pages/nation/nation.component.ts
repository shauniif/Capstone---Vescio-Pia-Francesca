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
    this.nationSvc.getAll().subscribe
    (nations => {
      this.nations = nations;
      console.log("Nazioni ricevute:", this.nations)
    },
    error => {
      console.error('Errore durante il recupero delle nazioni:', error.message);
    }
  )
  console.log("ciao");
  }

}
