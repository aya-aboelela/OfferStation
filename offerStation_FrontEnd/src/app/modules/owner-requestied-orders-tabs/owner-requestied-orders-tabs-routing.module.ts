import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerRequestedOrdersTabsComponent } from 'src/app/pages/owner-requested-orders-tabs/owner-requested-orders-tabs.component';

const routes: Routes = [
  {
    path: '', component: OwnerRequestedOrdersTabsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerRequestiedOrdersTabsRoutingModule { }
