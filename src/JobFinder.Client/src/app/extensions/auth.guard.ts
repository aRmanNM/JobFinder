import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AppService } from '../app.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private appService: AppService, private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.appService.loggedIn()) {
            return true;
        }

        this.router.navigate(['/auth'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}