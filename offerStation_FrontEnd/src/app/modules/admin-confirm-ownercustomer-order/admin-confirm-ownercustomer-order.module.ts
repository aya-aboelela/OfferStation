import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AdminAssignDeliveryOrderModule } from '../admin-assign-delivery-order/admin-assign-delivery-order.module';
import { DataTablesModule } from 'angular-datatables';
import { AdminConfirmOwnercustomerOrderComponent } from 'src/app/pages/admin-confirm-ownercustomer-order/admin-confirm-ownercustomer-order.component';


const routes: Routes = [
  { path: '', component:AdminConfirmOwnercustomerOrderComponent},
];

@NgModule({
  declarations: [AdminConfirmOwnercustomerOrderComponent],
  imports: [RouterModule.forChild(routes),
    AdminAssignDeliveryOrderModule,
    DataTablesModule,
    CommonModule,

  ],
  
})

export class AdminConfirmOwnercustomerOrderModule { }
