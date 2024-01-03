import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierRequestedOrdersComponent } from 'src/app/pages/supplier-requested-orders/supplier-requested-orders.component';

const routes: Routes = [
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupplierRequestiedOrdersRoutingModule { }
