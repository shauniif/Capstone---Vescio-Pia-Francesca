import { ActivatedRoute, Route } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { iCharacter } from '../../../interfaces/i-character';
import { CharacterRoutingModule } from '../character-routing.module';
import { CharacterService } from '../../../Services/character.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class DetailComponent implements OnInit {

  currCharacter!: iCharacter;
  constructor(private characterSvc: CharacterService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      this.characterSvc.GetCharacter(params.id).subscribe(character => {
        this.currCharacter = character;

      })
    })
  }
}
