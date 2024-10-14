import { CharacterService } from './../../../Services/character.service';
import { CharacterComponent } from './../../character/character.component';
import { Component, OnInit } from '@angular/core';
import { GuildService } from '../../../Services/guild.service';
import { ActivatedRoute } from '@angular/router';
import { iCharacter } from '../../../interfaces/i-character';
import { GuildRoleService } from '../../../Services/guild-role.service';
import { iGuildRole } from '../../../interfaces/i-guild-role';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-guildmembers',
  templateUrl: './guildmembers.component.html',
  styleUrl: './guildmembers.component.scss'
})
export class GuildmembersComponent implements OnInit  {

  characters: iCharacter[] = [];
  guildRoles: iGuildRole[] = [];
  guildRoleForm!: FormGroup;
  constructor(private guildRolesSvc: GuildRoleService, private CharacterService: CharacterService, private route: ActivatedRoute, private fb: FormBuilder, private modalService: NgbModal) {
  }


  ngOnInit(): void {

    this.route.params.subscribe((params:any) => {
     this.CharacterService.characters$.subscribe(characters => {
      this.characters = characters.filter(character => character.guild?.id == params.id);

     })
     this.guildRolesSvc.guildRoles$.subscribe(guildRoles => {
      this.guildRoles = guildRoles.filter(guildRole => guildRole.guild.id == params.id)
    })
    this.guildRoleForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      modifier: this.fb.control(null, [Validators.required]),
      guildId: this.fb.control(Number(params.id), [Validators.required]),
    })
    })

  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  createorUpdateRole() {
    if(this.guildRoleForm.valid) {
      const formData = new FormData();
      formData.append('name', this.guildRoleForm.get('name')?.value);
      formData.append('modifier', this.guildRoleForm.get('modifier')?.value);
      formData.append('guildId', this.guildRoleForm.get('guildId')?.value);
      this.guildRolesSvc.CreateGuildRole(formData).subscribe(role => {
        this.guildRoles.push(role)
        this.guildRoleForm.reset();
        this.modalService.dismissAll();
      })
    }

  }

  isTouchedInvalid(fieldName:string){
    const field = this.guildRoleForm.get(fieldName);
    return field?.invalid && field?.touched
  }
}
