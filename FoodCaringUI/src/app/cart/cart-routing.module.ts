import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShoppingCartComponent } from './shopping.cart.component';
import { CheckoutComponent } from "./checkout.component";

const routes: Routes = [
    { path: '', component: ShoppingCartComponent },
    { path: 'checkout', component: CheckoutComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CartRoutingModule { }