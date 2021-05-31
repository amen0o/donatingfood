import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from '../_services';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ManagerGuard implements CanActivate {
    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.accountService.userValue;
        if (user && user.role 
            && user.role == environment.roles[0]
            || user.role == environment.roles[1]) {
            // user is Administrator or Manager role so return ture
            return true;
        }

        // not in Administrator or Manager
        this.router.navigate([''], { queryParams: {} });
        return false;
    }
}