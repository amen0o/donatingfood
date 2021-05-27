import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { ProductService, RestaurantService, AlertService } from '../_services';
import { environment } from '../../environments/environment';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
    form: FormGroup;
    id: string;
    isAddMode: boolean;
    loading = false;
    submitted = false;
    restaurants = [];

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private productService: ProductService,
        private restaurantService: RestaurantService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.isAddMode = !this.id;

        this.form = this.formBuilder.group({
            name: ['', Validators.required],
        });

        if (!this.isAddMode) {
            this.restaurantService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                    this.f.name.setValue(x.name);
                });
        }

        this.restaurantService.getAll()
            .pipe(first())
            .subscribe(restaurants => {
                this.restaurants = restaurants;
            });
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
        if (this.isAddMode) {
            this.createRestaurant();
        } else {
            this.updateRestaurant();
        }
    }

    private createRestaurant() {
        this.restaurantService.create(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Restaurant added successfully', { keepAfterRouteChange: true });
                    this.router.navigate(['.', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    private updateRestaurant() {
        this.restaurantService.update(this.id, this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Update successful', { keepAfterRouteChange: true });
                    this.router.navigate(['..', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}