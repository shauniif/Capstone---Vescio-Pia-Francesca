import { CharacterService } from './../../../Services/character.service';
import { CharacterComponent } from './../../character/character.component';
import { Component, OnInit } from '@angular/core';
import { GuildService } from '../../../Services/guild.service';
import { ActivatedRoute } from '@angular/router';
import { iCharacter } from '../../../interfaces/i-character';

@Component({
  selector: 'app-guildmembers',
  templateUrl: './guildmembers.component.html',
  styleUrl: './guildmembers.component.scss'
})
export class GuildmembersComponent implements OnInit  {

  characters: iCharacter[] = [];
  constructor(private guildSvc: GuildService, private CharacterService: CharacterService, private route: ActivatedRoute) {
  }


  ngOnInit(): void {
    this.route.params.subscribe((params:any) => {
     this.CharacterService.characters$.subscribe(characters => {
      console.log(characters);
      this.characters = characters.filter(character => character.guild?.id == params.id);
      console.log(this.characters);
     })})
  }
}
