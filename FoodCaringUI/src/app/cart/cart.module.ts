import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartRoutingModule } from "src/app/cart/cart-routing.module";

import { ShoppingCartComponent } from "./shopping.cart.component";
import { CheckoutComponent } from "./checkout.component";

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CartRoutingModule
    ],
    declarations: [
        ShoppingCartComponent,
        CheckoutComponent
    ]
})
export class CartModule { }