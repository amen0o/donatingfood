import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../_models';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '../_services';

@Component({ templateUrl: 'details.component.html' })
export class DetailsComponent implements OnInit {
    id: string;
    user: User;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit(): void {
        this.id = this.route.snapshot.params['id'];

        this.accountService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                    this.user = x;
                });
    }

    private updateIntolerances() {
        this.accountService.update(this.id, [])
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Update successful', { keepAfterRouteChange: true });
                    this.router.navigate(['..', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                });
    }
}