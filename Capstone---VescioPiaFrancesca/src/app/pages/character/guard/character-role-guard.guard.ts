import { iUser } from './../../../interfaces/i-user';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { CharacterService } from '../../../Services/character.service';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CharacterRoleGuardGuard  {
  constructor(
    private authSvc: AuthService,
    private characterSvc: CharacterService, // Servizio che gestisce i personaggi
    private router: Router
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    const userid = this.authSvc.GetId();
    const guildId = route.paramMap.get('id');
    return this.characterSvc.GetMyCharacter(userid).pipe(
      map(characters => {
        // controllo se almeno un dei personaggi dell'utente loggato ha sia il ruolo Leader ma anche che appartenga alla gilda "cercata"
        const hasRequiredRole = characters.some(character => character.guildRole?.name == 'Leader' && character.guild?.id === Number(guildId));

        if(hasRequiredRole) {
          return true;
        } else {
          this.router.navigate(['/page401']);
          return false;
        }
      }
      )
    )
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    return this.canActivate(childRoute, state);
  }

}
