import { Component } from '@angular/core';
import { iGuild } from '../../interfaces/i-guild';
import { GuildService } from '../../Services/guild.service';
import { NationsService } from '../../Services/nations.service';
import { map } from 'rxjs';
import { CharacterService } from '../../Services/character.service';

@Component({
  selector: 'app-guild',
  templateUrl: './guild.component.html',
  styleUrl: './guild.component.scss'
})
export class GuildComponent {

  guilds: iGuild[] = [];
  nationsName: string[] = [];
  filteredGuilds: iGuild[] = [];
  guildsCopy: iGuild[] = [];
  isCollapsed: boolean = true;
  isCollapsed2: boolean = true;
  constructor(private guildSvc: GuildService, private nationSvc: NationsService, private characterSvc: CharacterService) {}
  ngOnInit(): void {

    this.guildSvc.guilds$.subscribe((guilds) =>{
      this.guilds = guilds;
      this.filteredGuilds = [...this.guilds]
      })



      this.getNationName()
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


  FilterGuild(name: string): void {
    this.filteredGuilds = this.guilds.filter(guild => guild.nation.name === name)
  }

  OrderGuildByName(): void {
    this.filteredGuilds.sort((a, b) => a.name.localeCompare(b.name))
  }

  OrderGuildByPower(): void {
    this.filteredGuilds.sort((a, b) => b.power - a.power)
  }

  DropDownClose(dropdownType: 'first' | 'second'): void {
    if (dropdownType === 'first') {
      this.isCollapsed = true;
    } else if (dropdownType === 'second') {
      this.isCollapsed2 = true;
    }
  }
}
