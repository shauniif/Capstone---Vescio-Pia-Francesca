import { Component } from '@angular/core';
import { iGuild } from '../../../interfaces/i-guild';
import { ActivatedRoute } from '@angular/router';
import { GuildService } from '../../../Services/guild.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {

  guild!: iGuild
  constructor(private route:ActivatedRoute, private guildSvc: GuildService) {}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.guildSvc.getGuild(params.id).subscribe(guild => {
        this.guild = guild;
      })
    })
  }
}

