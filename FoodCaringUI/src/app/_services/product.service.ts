import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Product } from '../_models';

@Injectable({ providedIn: 'root' })
export class ProductService {
    constructor(
        private http: HttpClient
    ) { }

    getAll() {
        return this.http.get<Product[]>(`${environment.apiUrl}/product`);
    }

    getById(id: string) {
        return this.http.get<Product>(`${environment.apiUrl}/product/${id}`);
    }

    create(product: Product) {
        return this.http.post(`${environment.apiUrl}/product/create`, product);
    }

    update(id: string, params: []) {
        return this.http.put(`${environment.apiUrl}/product/${id}`, params)
            .pipe(map(x => {
                return x;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/product/${id}`)
            .pipe(map(x => {
                return x;
            }));
    }
}