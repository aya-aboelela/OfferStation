import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerRequestedOrdersComponent } from 'src/app/pages/owner-requested-orders/owner-requested-orders.component';

const routes: Routes = [
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerRequestiedOrdersRoutingModule { }
