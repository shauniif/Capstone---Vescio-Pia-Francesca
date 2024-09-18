import { CharacterService } from './../../../Services/character.service';
import { Component, OnInit } from '@angular/core';
import { iUser } from '../../../interfaces/i-user';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit{

  user!: iUser;
  imageForm!: FormGroup;
  formattedDate!: string;
  constructor(private authSvc: AuthService, private fb:FormBuilder, private characterSvc: CharacterService) {}

  ngOnInit(): void {
  this.authSvc.user$.subscribe(user => {
      if(user) this.user = user;

      this.characterSvc.GetMyCharacter(this.user.id).subscribe(characters => {
        this.user.characters = characters;
        console.log(this.user.characters)
      })
      this.imageForm = this.fb.group({
        id: this.fb.control(this.user.id, [Validators.required]),
        image: this.fb.control(null, [Validators.required]),
      })
    })



  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];

    this.imageForm.patchValue({ image: file });
    this.imageForm.get('image')?.updateValueAndValidity();

  }

  UploadImage(): void {
    if (this.imageForm.valid &&  this.imageForm.get('image')?.value) {
      const formData: FormData = new FormData();
      formData.append('id', this.imageForm.get('id')?.value);
      formData.append('image',  this.imageForm.get('image')?.value);

      this.authSvc.insertImage(formData).subscribe({
        next: (response) => {
          console.log('Immagine caricata con successo:', response)
          this.user.image = response.image;
        },
        error: (err: HttpErrorResponse) => {
          console.error('Errore durante il caricamento dell\'immagine:', err);
        }
      });
    } else {
      console.error('Form non valido o nessun file selezionato.');
    }
  }

  DeleteCharacter(id:number): void {
    this.characterSvc.DeleteCharacter(id).subscribe(() => {

      this.user.characters= this.user.characters.filter(c => c.id!== id);
    })
  }

  logout() : void
  {
   this.authSvc.logout();
  }

}



