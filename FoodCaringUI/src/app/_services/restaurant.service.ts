import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Restaurant } from '../_models';

@Injectable({ providedIn: 'root' })
export class RestaurantService {
    constructor(
        private http: HttpClient
    ) { }

    getAll() {
        return this.http.get<Restaurant[]>(`${environment.apiUrl}/restaurant`);
    }

    getAllWithProducts() {
        return this.http.get<Restaurant[]>(`${environment.apiUrl}/restaurant/all`);
    }

    getById(id: string) {
        return this.http.get<Restaurant>(`${environment.apiUrl}/restaurant/${id}`);
    }

    create(restaurant: Restaurant) {
        return this.http.post(`${environment.apiUrl}/restaurant/create`, restaurant);
    }

    update(id: string, params: []) {
        return this.http.put(`${environment.apiUrl}/restaurant/${id}`, params)
            .pipe(map(x => {
                return x;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/restaurant/${id}`)
            .pipe(map(x => {
                return x;
            }));
    }
}