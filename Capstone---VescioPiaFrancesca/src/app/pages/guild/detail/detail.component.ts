import { Component } from '@angular/core';
import { iGuild } from '../../../interfaces/i-guild';
import { ActivatedRoute } from '@angular/router';
import { GuildService } from '../../../Services/guild.service';
import { CharacterService } from '../../../Services/character.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent {

  guild!: iGuild
  requiredRole: boolean = false;
  constructor(private route:ActivatedRoute, private guildSvc: GuildService, private chacterSvc: CharacterService, private authSvc: AuthService) {}
  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
      this.guildSvc.guilds$.subscribe(guilds => {
        let guild = guilds.find(g => g.id == params.id);
        if(guild) this.guild = guild;
      })
    })
    this.HasRole();
  }

  HasRole() : void {
    const userId = this.authSvc.GetId();

    this.chacterSvc.characters$.subscribe(characters => {

      let charactersUser = characters.filter(character => character.user.id === userId) && characters.filter(character => character.guild?.id == this.guild.id);

      this.requiredRole = charactersUser.some(character => character.guildRole?.name === "Leader")
     })
  }
}

