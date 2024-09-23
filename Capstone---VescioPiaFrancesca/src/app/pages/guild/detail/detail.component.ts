import { Component } from '@angular/core';
import { iGuild } from '../../../interfaces/i-guild';
import { ActivatedRoute } from '@angular/router';
import { GuildService } from '../../../Services/guild.service';
import { CharacterService } from '../../../Services/character.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {

  guild!: iGuild
  constructor(private route:ActivatedRoute, private guildSvc: GuildService, private chacterSvc: CharacterService) {}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.guildSvc.guilds$.subscribe(guilds => {
        let guild = guilds.find(g => g.id == params.id);
        if(guild) this.guild = guild;
      })
    })
  }
}

