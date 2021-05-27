import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { RestaurantService } from '../_services';
import { environment } from '../../environments/environment';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    restaurants = null;

    constructor(private restaurantService: RestaurantService) { }

    ngOnInit() {
        this.restaurantService.getAll()
            .pipe(first())
            .subscribe(restaurants => {
                this.restaurants = restaurants;
            });
    }

    deleteRestaurant(id: string) {
        const restaurant = this.restaurants.find(x => x.id === id);
        restaurant.isDeleting = true;
        this.restaurantService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.restaurants = this.restaurants.filter(x => x.id !== id);
            });
    }
}