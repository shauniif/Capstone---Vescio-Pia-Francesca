import { iGuildRole } from './../interfaces/i-guild-role';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, tap, Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GuildRoleService {

  constructor(private http: HttpClient)
  {
    this.getAll();
  }

  guildRoles: iGuildRole[] = [];
  private guildRolesSubject = new BehaviorSubject<iGuildRole[]>([]);
  guildRoles$ = this.guildRolesSubject.asObservable();
  guildRoleUrl:string = `${environment.apiUrl}GuildRoleApi`


  getAll()
  {
    return this.http
    .get<iGuildRole[]>(this.guildRoleUrl)
    .subscribe((data) => {
      this.guildRoles = data
      this.guildRolesSubject.next(this.guildRoles);
    })}


    CreateGuildRole(guildRole:FormData) : Observable<iGuildRole> {
      return this.http.post<iGuildRole>(`${this.guildRoleUrl}/Create`, guildRole).pipe(
        tap((newGuildRole) => {
          this.guildRoles.push(newGuildRole);
          this.guildRolesSubject.next(this.guildRoles);
        })
      )
    }

    DeleteGuildRole(id:number)
    {
      this.http.delete<iGuildRole>(`${this.guildRoleUrl}/${id})}`).pipe(
        tap(() => {
          const index = this.guildRoles.findIndex(gr => gr.id === id);
          this.guildRoles.splice(index, 1);
          this.guildRolesSubject.next(this.guildRoles);
        })
      )
    }

    UpdateGuildRole(id:number, updatedGuildRole: FormData)
    {
      this.http.put<iGuildRole>(`${this.guildRoleUrl}/${id}`, updatedGuildRole).pipe(
        tap((updatedRole) => {
          const index = this.guildRoles.findIndex(gr => gr.id === updatedRole.id);
          this.guildRoles[index] = updatedRole;
          this.guildRolesSubject.next(this.guildRoles);
        })
      )
    }

  }

