import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { iNations } from '../../interfaces/nations';
import { NationsService } from '../../Services/nations.service';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {

  currNation!: iNations
  constructor(
    private route:ActivatedRoute,
    private nationSvc: NationsService
  ){}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.nationSvc.getNation(id).subscribe(nation => {
      this.currNation = nation;
      console.log('Nazione trovata:', this.currNation);
    });
  }

}
