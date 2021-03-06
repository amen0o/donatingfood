import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../_services';
import { environment } from '../../environments/environment';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    administratorRole = environment.roles[0];
    managerRole = environment.roles[1];
    donatorRole = environment.roles[2];
    disadvangedRole = environment.roles[3];
    users = null;
    administratorUsers = null;
    managerUsers = null;
    disadvantagedUsers = null;
    donatorUsers = null;

    constructor(private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getAll()
            .pipe(first())
            .subscribe(users => {
                this.users = users;
                this.administratorUsers = users.filter(x => x.role == this.administratorRole);
                this.managerUsers = users.filter(x => x.role == this.managerRole);
                this.donatorUsers = users.filter(x => x.role == this.donatorRole);
                this.disadvantagedUsers = users.filter(x => x.role == this.disadvangedRole);
                this.sortDisadvantagedUsers();
            });
    }

    deleteUser(id: string) {
        const user = this.users.find(x => x.id === id);
        user.isDeleting = true;
        this.accountService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.users = this.users.filter(x => x.id !== id);
                this.administratorUsers = this.administratorUsers.filter(x => x.id !== id);
                this.managerUsers = this.managerUsers.filter(x => x.id !== id);
                this.donatorUsers = this.donatorUsers.filter(x => x.id !== id);
                this.disadvantagedUsers = this.disadvantagedUsers.filter(x => x.id !== id);
            });
    }

    increasePriority(id: string) {
        this.modifyPriority(id, environment.priorityModifier);
    }

    decreasePriority(id: string) {
        this.modifyPriority(id, -environment.priorityModifier);
    }

    private modifyPriority(id: string, priorityModifier: number){
        const user = this.users.find(x => x.id === id);
        this.accountService.modifyPriority(id, priorityModifier)
            .pipe(first())
            .subscribe(() => {
                user.priorityComputed += priorityModifier;
                this.sortDisadvantagedUsers();
            });
    }

    private sortDisadvantagedUsers() {
        this.disadvantagedUsers = this.disadvantagedUsers
            .sort((a, b) => (a.priorityComputed > b.priorityComputed) ? 1 : -1);
    }
}