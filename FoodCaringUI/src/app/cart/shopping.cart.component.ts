import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { OrderService, AccountService} from '../_services';
import { User } from '../_models';

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
        this.order = this.orderService.getOrder(this.user.Id);
    }
 }