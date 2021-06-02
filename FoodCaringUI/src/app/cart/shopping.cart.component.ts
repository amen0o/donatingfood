import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { OrderService, AccountService} from '../_services';
import { User } from '../_models';
import { first } from 'rxjs/operators';

@Component({ templateUrl: 'shopping.cart.component.html' })
export class ShoppingCartComponent implements OnInit {
    order = null;
    user = null;

    constructor(
        private orderService: OrderService,
        private accountService: AccountService) {
        
    }

    ngOnInit(): void {
        this.user = this.accountService.userValue;
        this.orderService.getOrder(this.user.id)
        .pipe(first())
        .subscribe(x => this.order = x);
    }
 }