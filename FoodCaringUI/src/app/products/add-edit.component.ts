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
            title: ['', Validators.required],
            image: ['', Validators.required],
            price: ['', Validators.required],
            restaurantId: ['', Validators.required]
        });

        if (!this.isAddMode) {
            this.productService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                    this.f.title.setValue(x.title);
                    this.f.image.setValue(x.image);
                    this.f.price.setValue(x.price);
                    this.f.restaurantId.setValue(x.restaurant.id);
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
            this.createProduct();
        } else {
            this.updateProduct();
        }
    }

    private createProduct() {
        this.productService.create(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Product added successfully', { keepAfterRouteChange: true });
                    this.router.navigate(['.', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    private updateProduct() {
        this.productService.update(this.id, this.form.value)
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

    changeRestaurant(e) {
        this.f.restaurantId.setValue(e.target.value, { onlySelf: true });
    }
}