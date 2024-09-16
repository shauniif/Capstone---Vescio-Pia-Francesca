import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, Observable } from 'rxjs';
import { iCharacter } from '../interfaces/i-character';
import { ICharacterCreate } from '../interfaces/i-character-create';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  constructor(private http: HttpClient) {

    this.GetAll();
  }
  private charactersSubject = new BehaviorSubject<iCharacter[]>([]);
  characters$ = this.charactersSubject.asObservable();
  characters: iCharacter[] = []

  characterUrl: string = `${environment.apiUrl}CharacterApi`;

  GetAll(){
    return this.http.get<iCharacter[]>(this.characterUrl)
    .subscribe((data) => {
      this.characters = data
      this.charactersSubject.next(this.characters);
    })
  }

  GetMyCharacter(id: number) : Observable<iCharacter[]> {
    return this.http.get<iCharacter[]>(`${this.characterUrl}/OfUser/${id}`);
  }

  GetCharacter(id: number) : Observable<iCharacter> {
    return this.http.get<iCharacter>(`${this.characterUrl}/${id}`);
  }



  CreateCharacter(newCharacter: FormData) : Observable<iCharacter>{
    return this.http.post<iCharacter>(`${this.characterUrl}/Create`, newCharacter)
  }

  UpdateCharacter(id:number,currCharacter: FormData) : Observable<iCharacter>{
    return this.http.put<iCharacter>(`${this.characterUrl}/${id}`, currCharacter)
  }

  DeleteCharacter(id:number) {
    return this.http.delete<iCharacter>(`${this.characterUrl}/${id}`);
  }


}
