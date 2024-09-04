import { CityService } from './../../../Services/city.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { iUser } from '../../../interfaces/i-user';
import { iCharacter } from '../../../interfaces/i-character';
import { ICharacterCreate } from '../../../interfaces/i-character-create';
import { CharacterService } from '../../../Services/character.service';
import { RaceService } from '../../../Services/race.service';
import { iRace } from '../../../interfaces/i-race';
import { EcosService } from '../../../Services/ecos.service';
import { iEco } from '../../../interfaces/i-eco';
import { GuildService } from '../../../Services/guild.service';
import { iGuild } from '../../../interfaces/i-guild';
import { iCity } from '../../../interfaces/i-city';
import { map } from 'rxjs';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent implements OnInit {
  user!: iUser;
  createCharacterForm!: FormGroup
  newCharacter!: ICharacterCreate
  races: iRace[] = [];
  ecos: iEco[] = [];
  guilds: iGuild[] = [];
  cities: iCity[] = [];
  constructor(private fb: FormBuilder, private authSvc: AuthService, private characterSvc: CharacterService, private raceSvc: RaceService, private ecoSvc: EcosService, private guildSvc: GuildService, private citySvc: CityService ) {

  }
  ngOnInit(): void {
    this.authSvc.user$.subscribe(user => {
      if(user)this.user = user;
      this.createCharacterForm = this.fb.group({
        name: this.fb.control(null, [Validators.required]),
        guildId: this.fb.control(null ),
        cityId: this.fb.control(null, [Validators.required]),
        raceId: this.fb.control(null, [Validators.required]),
        ecoId: this.fb.control(null ),
        userId: this.fb.control(this.user.id, [Validators.required]),
        image: this.fb.control(null, [Validators.required]),
      });
    })

    this.getRaces()
    this.getEco()
    this.getGuilds()
    this.getCities()
  }



 onFileSelected(event: any): void {
    const file = event.target.files[0];
    this.createCharacterForm.patchValue({ image: file });
    this.createCharacterForm.get('image')?.updateValueAndValidity();
  }

  isTouchedInvalid(fieldName:string){
    const field = this.createCharacterForm.get(fieldName);
    return field?.invalid && field?.touched
  }

  CreateCaracter(): void {
    if(this.createCharacterForm.valid) {
      const formData = new FormData();

    // Aggiungi i dati del modulo
    formData.append('name', this.createCharacterForm.get('name')?.value);
    formData.append('cityId', this.createCharacterForm.get('cityId')?.value);
    formData.append('raceId', this.createCharacterForm.get('raceId')?.value);
    formData.append('userId', this.createCharacterForm.get('userId')?.value);
    formData.append('image', this.createCharacterForm.get('image')?.value);

    const guildId = this.createCharacterForm.get('guildId')?.value;
    if (guildId) {
      formData.append('guildId', guildId);
    }

    const ecoId = this.createCharacterForm.get('ecoId')?.value;
    if (ecoId) {
      formData.append('ecoId', ecoId);
    }

    this.characterSvc.CreateCharacter(formData).subscribe((character) =>
        {
          console.log("Personaggio creato con successo:", character);
          this.createCharacterForm.reset();
        }
      )
    }
  }

  getRaces(): void {
    this.raceSvc.getAll().subscribe((races) => {
      this.races = races;
    })
  }

  getEco(): void {
    this.ecoSvc.getAll().subscribe((ecos) => {
      this.ecos = ecos;
    })
  }

  onGuildChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    console.log('Event target:', target);  // Aggiungi questo
    console.log('Selected value:', target.value);
    if (target) {
      const selectedGuildId = parseInt(target.value, 10);
      console.log(selectedGuildId)
      const selectedGuild = this.guilds.find(guild => guild.id === selectedGuildId);
      console.log(selectedGuild?.name)
      if (selectedGuild) {
        const nationId = selectedGuild.nation.id;
        console.log(nationId)
        // Filtra le città basate sulla nazione
        this.getCitiesByNation(nationId);
      }
    } else {
      this.getCities(); // Gestisci il caso in cui non ci sono città da filtrare
    }
  }

  onCityChange(event: Event): void {
    const target = event.target as HTMLSelectElement; // Casting dell'event.target
    if (target) {
      const selectedCityId = parseInt(target.value, 10);
      console.log(selectedCityId)
      const selectedCity = this.cities.find(city => city.id === selectedCityId);
      console.log(selectedCity?.name)
      if (selectedCity) {
        const nationId = selectedCity.nation.id;
        // Filtra le gilde basate sulla nazione
        this.getGuildsByNation(nationId);
      }
    } else {
      this.getGuilds(); // Gestisci il caso in cui non ci sono gilde da filtrare
    }
    }

  getGuilds(): void {
    this.guildSvc.getAll()
    .subscribe((guilds) => {
      this.guilds = guilds
    })
  }

  getCities(): void {
    this.citySvc.getAll()
    .subscribe((cities) => {
         this.cities = cities
        })
  }


  getGuildsByNation(id: number): void {
    this.guildSvc.getAll()
    .pipe(
      map(guilds => guilds.filter(guild => guild.nation.id === id))
    )
    .subscribe((filteredGuilds) => {
      this.guilds = filteredGuilds;
    })
  }

  getCitiesByNation(id: number): void {
    this.citySvc.getAll()
    .pipe(
      map(cities => cities.filter(city => city.nation.id === id))
    )
    .subscribe((filteredCities) => {
      this.cities = filteredCities;
    })
  }


}

