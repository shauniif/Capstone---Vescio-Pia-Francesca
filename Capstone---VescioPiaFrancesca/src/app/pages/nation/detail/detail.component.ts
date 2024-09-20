import { Component } from '@angular/core';
import { iNation } from '../../../interfaces/i-nation';
import { ActivatedRoute } from '@angular/router';
import { NationsService } from '../../../Services/nations.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {
  nation!: iNation | undefined;
  isCollapsed: boolean = true
  isCollapsed2: boolean = true
  isCollapsed3: boolean = true
  constructor(
    private route:ActivatedRoute,
    private nationSvc: NationsService
  ){}

  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.nationSvc.nations$.subscribe(nations => {
         this.nation = nations.find(nation => nation.id == params.id);
      })
      })
    }
  }
