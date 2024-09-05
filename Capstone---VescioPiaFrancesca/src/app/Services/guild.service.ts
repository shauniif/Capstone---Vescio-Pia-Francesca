import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { iGuild } from '../interfaces/i-guild';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuildService {

  constructor(private http: HttpClient)
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
      this.guildsSubject.next(this.guilds);
    })
  }

  getGuild(id: number) {
    return this.http.get<iGuild>(`${this.guildUrl}/${id}`)
  }


}
