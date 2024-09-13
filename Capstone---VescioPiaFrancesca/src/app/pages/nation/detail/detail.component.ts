import { Component } from '@angular/core';
import { iNations } from '../../../interfaces/nations';
import { ActivatedRoute } from '@angular/router';
import { NationsService } from '../../../Services/nations.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {
  nation!: iNations
  isCollapsed: boolean = true
  isCollapsed2: boolean = true
  isCollapsed3: boolean = true
  constructor(
    private route:ActivatedRoute,
    private nationSvc: NationsService
  ){}

  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {

      this.nationSvc.getNation(params.id).subscribe(nation => {
        this.nation = nation;
      })
    })
  }

}
