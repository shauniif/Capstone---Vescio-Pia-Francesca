import { Component } from '@angular/core';
import { iEco } from '../../interfaces/i-eco';
import { EcosService } from '../../Services/ecos.service';
import { NationsService } from '../../Services/nations.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-eco',
  templateUrl: './eco.component.html',
  styleUrl: './eco.component.scss'
})
export class EcoComponent {

  ecos: iEco[] = [];
  nationsName: string[] = [];
  filteredEcos: iEco[] = [];
  EcosWithNoNation: iEco[] = [];
  isCollapsed: boolean = true;
  isCollapsed2: boolean = true;
  constructor(private ecoSvc: EcosService, private nationSvc: NationsService)
  {

  }

  ngOnInit(): void {
      this.ecoSvc.ecos$.subscribe((ecos) => {
        this.ecos = ecos;
        console.log(this.ecos);
        this.filteredEcos = [... this.ecos]
        this.EcosWithNoNation = this.filteredEcos.filter(eco => eco.nation == null)

    })

    this.getNationName();
  }

  getNationName(): void {
    this.nationSvc.nations$
    .pipe(
       map(nations => nations.map(nation => nation.name)
    ))
    .subscribe((nationsname) => {
      this.nationsName = nationsname
    });
  }


  FilterEco(name: string): void {
    this.filteredEcos = this.ecos
    this.filteredEcos.forEach((eco) => {
      if(eco.nation != null) {
        this.filteredEcos = this.ecos.filter(eco => eco.nation?.name == name)
      }
     })

  }

  OrderEcoByPosition(): void {
    this.filteredEcos.sort((a, b) => a.position - b.position)

  }

  OrderEcoByName(): void {
    this.filteredEcos.sort((a, b) => a.name.localeCompare(b.name))

  }

  DropDownClose(dropdownType: 'first' | 'second'): void {
    if (dropdownType === 'first') {
      this.isCollapsed = true;
      console.log(this.isCollapsed )
    } else if (dropdownType === 'second') {
      this.isCollapsed2 = true;
      console.log(this.isCollapsed2)
    }
  }
}
