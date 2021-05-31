import { Component } from '@angular/core';

import { AccountService } from './_services';
import { environment } from '../environments/environment';
import { User } from './_models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    user: User;
    isAdministrator: Boolean;
    isManager: Boolean;

    constructor(private accountService: AccountService) {
        this.accountService.user.subscribe(x => {
            this.user = x;
            this.isAdministrator = x && x.role && x.role == environment.roles[0];
            this.isManager = x && x.role && x.role == environment.roles[1];
        });
    }

    logout() {
        this.accountService.logout();
    }
}
