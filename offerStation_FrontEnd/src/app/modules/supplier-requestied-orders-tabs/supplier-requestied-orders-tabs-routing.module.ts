import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierRequestedOrdersTabsComponent } from 'src/app/pages/supplier-requested-orders-tabs/supplier-requested-orders-tabs.component';

const routes: Routes = [
  {path: '', component: SupplierRequestedOrdersTabsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupplierRequestiedOrdersTabsRoutingModule { }
