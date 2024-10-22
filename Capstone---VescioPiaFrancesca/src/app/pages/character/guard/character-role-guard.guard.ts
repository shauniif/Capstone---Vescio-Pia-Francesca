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
    return this.characterSvc.GetMyCharacter(userid).pipe(
      map(characters => {
        const hasRequiredRole = characters.some(character => character.guildRole?.name == 'Leader');

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
