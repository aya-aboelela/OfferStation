import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDeliveryComponent } from 'src/app/pages/admin/admin-delivery/admin-delivery.component';
import { AdminComponent } from 'src/app/pages/admin/admin-main/admin.component';
import { AdminOwnerCategoryComponent } from 'src/app/pages/admin/admin-owner-category/admin-owner-category.component';
import { AdminOwnerReviewsComponent } from 'src/app/pages/admin/admin-owner-reviews/admin-owner-reviews.component';
import { AdminReviewsComponent } from 'src/app/pages/admin/admin-reviews/admin-reviews.component';
import { AdminSupplierCategoryComponent } from 'src/app/pages/admin/admin-supplier-category/admin-supplier-category.component';
import { AdminWaitingOwnersComponent } from 'src/app/pages/admin/admin-waiting-owners/admin-waiting-owners.component';
import { AdminWaitingSuppliersComponent } from 'src/app/pages/admin/admin-waiting-suppliers/admin-waiting-suppliers.component';

const routes: Routes = [
  { path:'', component:AdminComponent, children: [
    { path: '', redirectTo: 'analysis', pathMatch: 'full' },
    { path:'ownerCategory', component:AdminOwnerCategoryComponent},
    { path:'supplierCategory', component:AdminSupplierCategoryComponent},
    { path:'delivery', component:AdminDeliveryComponent},
    { path:'ownerReview', component:AdminOwnerReviewsComponent},
    { path:'customerReview', component:AdminReviewsComponent},
    { path: 'userOrders', loadChildren: () => import('../admin-confirm-usercustomer-order/admin-confirm-usercustomer-order.module').then(mod => mod.AdminConfirmUsercustomerOrderModule) },
    { path: 'ownerOrders', loadChildren: () => import('../admin-confirm-ownercustomer-order/admin-confirm-ownercustomer-order.module').then(mod => mod.AdminConfirmOwnercustomerOrderModule) },

    { path:'waitingOwners', component:AdminWaitingOwnersComponent},
    { path:'waitingSuppliers', component:AdminWaitingSuppliersComponent},
    { path: 'analysis', loadChildren: () => import('../admin-dashboard/admin-dashboard.module').then(mod => mod.AdminDashboardModule) },
  ]}, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
