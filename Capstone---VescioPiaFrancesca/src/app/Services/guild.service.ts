import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iGuild } from '../interfaces/i-guild';

@Injectable({
  providedIn: 'root'
})
export class GuildService {

  constructor(private http: HttpClient)
  { }

  guildUrl:string = `${environment.apiUrl}GuildApi`

  getAll() {
  return this.http.get<iGuild[]>(this.guildUrl);
  }

  getGuild(id: number) {
    return this.http.get<iGuild>(`${this.guildUrl}/${id}`)
  }
}
