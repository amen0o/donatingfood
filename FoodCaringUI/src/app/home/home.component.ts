import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '../_models';
import { RestaurantService, OrderService, AccountService } from '../_services';
import { AlertService } from "src/app/_services/alert.service";

@Component({
    templateUrl: 'home.component.html',
})
export class HomeComponent {
    user: User;
    restaurantsWithProducts = null;

    constructor(
        private accountService: AccountService,
        private orderService: OrderService,
        private alertService: AlertService,
        private restaurantService: RestaurantService) {
        this.user = this.accountService.userValue;
    }

    ngOnInit() {
        this.restaurantService.getAllWithProducts()
            .pipe()
            .subscribe(x => { this.restaurantsWithProducts = x });
    }

    addToCart(productId) {
        this.orderService.addItemToOrder(this.user.id, productId)
        .pipe(first())
        .subscribe(data => {
            this.alertService.success('Product added to cart succesfully.', { keepAfterRouteChange: true });
        },
        error => {
            this.alertService.error(error);
        });
    }
}