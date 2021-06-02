import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';

import { User, Order } from '../_models';

import { AccountService, OrderService, AlertService } from '../_services';

@Component({ templateUrl: 'checkout.component.html' })
export class CheckoutComponent implements OnInit {
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
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.user = this.accountService.userValue;

        this.form = this.formBuilder.group({
            cardNumber: ['', Validators.required],
            cardCvv: ['', Validators.required],
            cardMonth: ['', Validators.required],
            cardYear: ['', Validators.required],
            userId: ['', Validators.required]
        });

        this.accountService.getAll()
            .pipe(first())
            .subscribe(x => {
                this.users = x;
            });

        this.orderService.getOrder(this.user.id)
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
            return;
        }

        this.loading = true;
        this.placeOrder();
    }

    placeOrder() {
        let userId = this.form.value.userId;
        this.orderService.placeOrder(userId, this.order.id)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Place order successful', { keepAfterRouteChange: true });
                    this.router.navigate(['..', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                });
    }

    changeUser(e) {
        this.f.userId.setValue(e.target.value, { onlySelf: true });
    }
}