import { Router, ActivatedRoute } from '@angular/router';
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
  character!: iCharacter;
  constructor(private fb: FormBuilder, private authSvc: AuthService, private characterSvc: CharacterService, private raceSvc: RaceService, private ecoSvc: EcosService, private guildSvc: GuildService, private citySvc: CityService, private route: ActivatedRoute, private router: Router) {

  }
  ngOnInit(): void {


    this.authSvc.user$.subscribe(user => {
      if(user)this.user = user;
      this.createCharacterForm = this.fb.group({
        name: this.fb.control(null, [Validators.required]),
        guildId: this.fb.control(''),
        cityId: this.fb.control(null, [Validators.required]),
        raceId: this.fb.control(null, [Validators.required]),
        ecoId: this.fb.control(''),
        userId: this.fb.control(this.user.id, [Validators.required]),
        image: this.fb.control('', [Validators.required]),
      });
    })


    this.createCharacterForm.get('guildId')?.valueChanges.subscribe((selectedGuildId: number) => {
      if (selectedGuildId) {
        this.onGuildChange(selectedGuildId);
      }
    });

    this.createCharacterForm.get('cityId')?.valueChanges.subscribe((selectedCityId: number)  => {
      if (selectedCityId) {
        this.onCityChange(selectedCityId);
      }
    });

    this.route.params.subscribe((params:any) => {;
      if(params) {
        this.characterSvc.GetCharacter(params.id).subscribe(character => {
          this.character = character;
          this.createCharacterForm.patchValue({
            name: this.character?.name,
            guildId: this.character?.guild?.id ? this.character?.guild?.id : '',
            cityId: this.character?.city?.id,
            raceId: this.character?.race?.id,
            ecoId: this.character?.eco?.id ? this.character?.eco?.id : '',
            userId: this.user?.id,
            image: '',
          });
        })
      }
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

    if (ecoId  != null) {
      formData.append('ecoId', ecoId);
    }
    if(this.character) {
      this.characterSvc.UpdateCharacter(this.character.id, formData).subscribe((character) =>
        {

          this.createCharacterForm.reset();
          this.router.navigate(['auth/profile']);
        }
      )
    } else {
      this.characterSvc.CreateCharacter(formData).subscribe((character) =>
          {

            this.createCharacterForm.reset();
            this.router.navigate(['auth/profile']);
          }
        )
      }
    }
  }

  getRaces(): void {
    this.raceSvc.getAll().subscribe((races) => {
      this.races = races;
    })
  }

  getEco(): void {
    this.ecoSvc.ecos$.subscribe((ecos) => {
      this.ecos = ecos;
    })
  }

  onGuildChange(selectedGuildId: number): void {

    const selectedGuild = this.guilds.find(guild => guild.id == selectedGuildId);
      if (selectedGuild) {
        const nationId = selectedGuild.nation.id;

        this.getCitiesByNation(nationId);

    } else {
      this.getCities();
    }
  }

  onCityChange(selectedCityId: number): void {
      const selectedCity = this.cities.find(city => city.id == selectedCityId)
      if (selectedCity) {
        const nationId = selectedCity.nation.id;
        // Filtra le gilde basate sulla nazione
        this.getGuildsByNation(nationId);
    } else {
      this.getGuilds();
    }
    }

  getGuilds(): void {
    this.guildSvc.guilds$.subscribe((guilds) => {
      this.guilds = guilds
    })
  }

  getCities(): void {
    this.citySvc.cities$.subscribe((cities) =>{
      this.cities = cities;
    })
  }


  getGuildsByNation(id: number): void {
    this.guildSvc.guilds$
    .pipe(
      map(guilds => guilds.filter(guild => guild.nation.id === id))
    )
    .subscribe((filteredGuilds) => {
      this.guilds = filteredGuilds;
    })
  }

  getCitiesByNation(id: number): void {
    this.citySvc.cities$
    .pipe(
      map(cities => cities.filter(city => city.nation.id === id))
    )
    .subscribe((filteredCities) =>{
      this.cities = filteredCities;
    })
  }


}

