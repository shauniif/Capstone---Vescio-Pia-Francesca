import { Component, OnInit } from '@angular/core';
import { iGuild } from '../../interfaces/i-guild';
import { GuildService } from '../../Services/guild.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit  {

  guild!: iGuild
  constructor(private route:ActivatedRoute, private guildSvc: GuildService) {}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      console.log("ID dal parametro della rotta:", params.id);
      this.guildSvc.getGuild(params.id).subscribe(guild => {
        this.guild = guild;
      })
    })
  }
}
