import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home';
import { AuthGuard, AdministratorGuard, ManagerGuard } from './_helpers';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./users/users.module').then(x => x.UsersModule);
const productsModule = () => import('./products/products.module').then(x => x.ProductsModule);
const restaurantsModule = () => import('./restaurants/restaurants.module').then(x => x.RestaurantsModule);
const cartModule = () => import('./cart/cart.module').then(x => x.CartModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard, AdministratorGuard] },
    { path: 'products', loadChildren: productsModule, canActivate: [AuthGuard, ManagerGuard] },
    { path: 'account', loadChildren: accountModule },
    { path: 'restaurants', loadChildren: restaurantsModule, canActivate: [AuthGuard, AdministratorGuard]},
    { path: 'cart', loadChildren: cartModule},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
