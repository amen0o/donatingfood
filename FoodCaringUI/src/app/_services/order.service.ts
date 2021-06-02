import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Order } from '../_models';

@Injectable({ providedIn: 'root' })
export class OrderService {
    constructor(
        private http: HttpClient
    ) { }

    getOrder(userId) {
        return this.http.get<Order[]>(`${environment.apiUrl}/order/${userId}`);
    }

    placeOrder(userId, orderId) {
        return this.http.post(`${environment.apiUrl}/order/placeOrder`, { userId, orderId });
    }
}