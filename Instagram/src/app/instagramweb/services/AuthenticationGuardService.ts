import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationStore } from './AuthenticationStore';

@Injectable()
export class AuthenticationGuardService implements CanActivate {

    constructor(
        public authenticationStore: AuthenticationStore,
        public router: Router) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): boolean {
        if (this.authenticationStore.isAuthenticated() !== true) {
            setTimeout(() => {
                this.router.navigate(['/register']);
            }, 100);
            return false;
        }

        return true;
    }

}
