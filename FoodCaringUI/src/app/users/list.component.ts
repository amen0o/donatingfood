import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../_services';
import { environment } from '../../environments/environment';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    managerRole = environment.roles[1];
    donerRole = environment.roles[2];
    disadvangedRole = environment.roles[3];
    users = null;
    managerUsers = null;
    disadvantagedUsers = null;
    donerUsers = null;

    constructor(private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getAll()
            .pipe(first())
            .subscribe(users => {
                this.users = users;
                this.managerUsers = users.filter(x => x.role == this.managerRole);
                this.donerUsers = users.filter(x => x.role == this.donerRole);
                this.disadvantagedUsers = users.filter(x => x.role == this.disadvangedRole);
            });
    }

    deleteUser(id: string) {
        const user = this.users.find(x => x.id === id);
        user.isDeleting = true;
        this.accountService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.users = this.users.filter(x => x.id !== id);
                this.managerUsers = this.managerUsers.filter(x => x.id !== id);
                this.donerUsers = this.donerUsers.filter(x => x.id !== id);
                this.disadvantagedUsers = this.disadvantagedUsers.filter(x => x.id !== id);
            });
    }
}