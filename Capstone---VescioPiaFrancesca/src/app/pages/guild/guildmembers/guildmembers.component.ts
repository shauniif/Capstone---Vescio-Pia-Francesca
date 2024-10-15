import { iGuildRole } from './../../../interfaces/i-guild-role';
import { CharacterService } from './../../../Services/character.service';
import { CharacterComponent } from './../../character/character.component';
import { Component, OnInit } from '@angular/core';
import { GuildService } from '../../../Services/guild.service';
import { ActivatedRoute } from '@angular/router';
import { iCharacter } from '../../../interfaces/i-character';
import { GuildRoleService } from '../../../Services/guild-role.service';
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
  idGuild!: string;
  guildRole!: iGuildRole | null;
  constructor(private guildRolesSvc: GuildRoleService, private CharacterService: CharacterService, private route: ActivatedRoute, private fb: FormBuilder, private modalService: NgbModal) {
  }


  ngOnInit(): void {

    this.route.params.subscribe((params:any) => {
      this.idGuild = params.id;
    })
     this.CharacterService.characters$.subscribe(characters => {
      this.characters = characters.filter(character => character.guild?.id == Number(this.idGuild));

     })
     this.guildRolesSvc.guildRoles$.subscribe(guildRoles => {
      this.guildRoles = guildRoles.filter(guildRole => guildRole.guild.id == Number(this.idGuild))
    })

    this.guildRoleForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      modifier: this.fb.control(null, [Validators.required]),
    })


  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  createorUpdateRole() {
   console.log(this.guildRoleForm.get('modifier')?.value);
   console.log(this.guildRoleForm.get('name')?.value);
   console.log(this.guildRoleForm.get('guildId')?.value);
    if(this.guildRoleForm.valid) {
      console.log("Form valido");
      const formData = new FormData();
      formData.append('name', this.guildRoleForm.get('name')?.value);
      formData.append('modifier', this.guildRoleForm.get('modifier')?.value);
      formData.append('guildId', this.idGuild)
      if(this.guildRole)  {
        this.guildRolesSvc.UpdateGuildRole(this.guildRole.id, formData).subscribe(role => {
          this.guildRoles = this.guildRoles.map(gr => gr.id === this.guildRole?.id? role : gr);
          this.ResetForm();
          this.guildRole = null;
        })
      } else {

        this.guildRolesSvc.CreateGuildRole(formData).subscribe(role => {
          this.ResetForm();
        })
      }
    }

  }

  isTouchedInvalid(fieldName:string){
    const field = this.guildRoleForm.get(fieldName);
    return field?.invalid && field?.touched
  }

  PrepareToUpdate(id: number) : void {
   let guildRole: iGuildRole | undefined = this.guildRoles.find(gr => gr.id === id);
    if(guildRole != undefined) this.guildRole = guildRole
    if(this.guildRole != null) {
      this.guildRoleForm.patchValue({
        name: this.guildRole.name,
        modifier: this.guildRole.modifier
      })
    }
  }

  Delete(id:number): void {
    this.guildRolesSvc.DeleteGuildRole(id).subscribe((guildRole) => {
      this.guildRoles = this.guildRoles.filter(gr => gr.id!== id);
    })
  }

  ResetForm() {
    this.guildRoleForm.reset();
    this.guildRole = null;
    this.modalService.dismissAll();
  }
}


