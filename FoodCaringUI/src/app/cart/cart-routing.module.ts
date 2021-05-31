import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShoppingCartComponent } from './shopping.cart.component';
const routes: Routes = [
    {
        path: '', component: ShoppingCartComponent,
        children: [
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CartRoutingModule { }