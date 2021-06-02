import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { User, Order } from '../_models';

import { AccountService, OrderService, AlertService } from '../_services';

@Component({ templateUrl: 'checkout.component.html' })
export class CheckoutComponent {
    user: User;
    submitted = false;
    order: any;
    users: any;
    form: FormGroup;
    loading: boolean;

    constructor(
        private accountService: AccountService,
        private alertService: AlertService,
        private orderService: OrderService,
        private formBuilder: FormBuilder
    ) { }

    ngOnInit(): void {
        this.user = this.accountService.userValue;

        this.form = this.formBuilder.group({
            userId: ['', Validators.required]
        });

        this.accountService.getAll()
            .pipe(first())
            .subscribe(x => {
                this.users = x;
            });

        this.orderService.getOrder()
            .pipe(first())
            .subscribe(x => this.order = x);
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            this.submitted = false;
            return;
        }

        this.loading = true;
        this.placeOrder();
    }

    placeOrder() {
        this.orderService.placeOrder()
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Place order successful', { keepAfterRouteChange: true });
                },
                error => {
                    this.alertService.error(error);
                });
    }

    changeUser(e) {
        this.f.userId.setValue(e.target.value, { onlySelf: true });
    }
}