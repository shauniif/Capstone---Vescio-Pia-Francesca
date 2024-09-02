import { Component, OnInit } from '@angular/core';
import { GuildService } from '../Services/guild.service';
import { iGuild } from '../interfaces/i-guild';

@Component({
  selector: 'app-guild',
  templateUrl: './guild.component.html',
  styleUrl: './guild.component.scss'
})
export class GuildComponent implements OnInit {

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
