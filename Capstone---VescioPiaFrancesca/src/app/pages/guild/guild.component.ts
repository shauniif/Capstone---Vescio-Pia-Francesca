import { Component } from '@angular/core';
import { iGuild } from '../../interfaces/i-guild';
import { GuildService } from '../../Services/guild.service';

@Component({
  selector: 'app-guild',
  templateUrl: './guild.component.html',
  styleUrl: './guild.component.scss'
})
export class GuildComponent {

  guilds: iGuild[] = [];
  constructor(private guildSvc: GuildService) {}
  ngOnInit(): void {
  this.guildSvc.getAll().subscribe(guilds => {
  this.guilds = guilds
  console.log("Ecos ricevuti:", this.guilds)
  },
  error => {
  console.error('Errore durante il recupero degli echi:', error.message);
  })
  }



}
