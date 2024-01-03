import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminConfirmUsercustomerOrderComponent } from 'src/app/pages/admin-confirm-usercustomer-order/admin-confirm-usercustomer-order.component';
import { AdminAssignDeliveryOrderModule } from '../admin-assign-delivery-order/admin-assign-delivery-order.module';
import { RouterModule, Routes } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { AdminAssignDeliveryOrderComponent } from 'src/app/pages/admin-assign-delivery-order/admin-assign-delivery-order.component';

const routes: Routes = [
  { path: '', component:AdminConfirmUsercustomerOrderComponent},
];

@NgModule({
  declarations: [AdminConfirmUsercustomerOrderComponent],
  imports: [RouterModule.forChild(routes),
    AdminAssignDeliveryOrderModule,
    DataTablesModule,
    CommonModule,

  ],
  exports: [
    AdminConfirmUsercustomerOrderComponent]
})


export class AdminConfirmUsercustomerOrderModule { }
