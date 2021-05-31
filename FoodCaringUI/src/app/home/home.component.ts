import { Component } from '@angular/core';

import { User } from '../_models';
import { AccountService } from '../_services';
import { RestaurantService } from '../_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    user: User;
    restaurantsWithProducts = null;

    constructor(
        private accountService: AccountService,
        private restaurantService: RestaurantService) {
        this.user = this.accountService.userValue;
    }

    ngOnInit() {
        this.restaurantService.getAllWithProducts()
            .pipe()
            .subscribe(x => { this.restaurantsWithProducts = x });
    }

    addToCart(id : string) {

    }
}