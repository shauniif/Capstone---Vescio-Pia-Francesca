import { CharacterService } from './character.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iGuild } from '../interfaces/i-guild';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuildService {

  constructor(private http: HttpClient, private CharacterSvc: CharacterService)
  {
    this.getAll();
  }
  guilds: iGuild[] = [];
  private guildsSubject = new BehaviorSubject<iGuild[]>([]);
  guilds$ = this.guildsSubject.asObservable();
  guildUrl:string = `${environment.apiUrl}GuildApi`

  getAll() {
    return this.http.get<iGuild[]>(this.guildUrl)
    .subscribe((data) => {
      this.guilds = data
      this.CharacterSvc.characters$.subscribe((character) => {
        console.log(character)
        this.guilds.forEach((guild) => {
        guild.members = character.filter((char) => char.guild?.id === guild.id);
        guild.power = 0
        guild.members.forEach((member) => {
          let score = Number(member.score)
          guild.power += score
        })
      })
      })
      this.guildsSubject.next(this.guilds);
    })
  }

  getGuild(id: number) {
    return this.http.get<iGuild>(`${this.guildUrl}/${id}`)
  }


}
