import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ShoppingCartComponent } from "./shopping.cart.component";
import { CartRoutingModule } from "src/app/cart/cart-routing.module";

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CartRoutingModule
    ],
    declarations: [
        ShoppingCartComponent
    ]
})
export class CartModule { }