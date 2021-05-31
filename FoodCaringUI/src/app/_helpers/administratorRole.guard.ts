import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from '../_services';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AdministratorGuard implements CanActivate {
    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.accountService.userValue;
        if (user && user.role && user.role == environment.roles[0]) {
            // user has 'Administrator' role so return ture
            return true;
        }

        // not in Administrator role
        this.router.navigate([''], { queryParams: {} });
        return false;
    }
}