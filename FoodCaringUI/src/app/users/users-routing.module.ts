import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
import { DetailsComponent } from './details.component';
import { AuthGuard, AdministratorGuard, ManagerGuard } from '../_helpers';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: ListComponent, canActivate: [AuthGuard, AdministratorGuard] },
            { path: 'add', component: AddEditComponent, canActivate: [AuthGuard, AdministratorGuard] },
            { path: 'edit/:id', component: AddEditComponent, canActivate: [AuthGuard, AdministratorGuard] },
            { path: 'details', component: DetailsComponent, canActivate: [AuthGuard] }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UsersRoutingModule { }