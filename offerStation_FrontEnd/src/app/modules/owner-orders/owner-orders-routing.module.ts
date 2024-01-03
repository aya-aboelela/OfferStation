import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerOrdersComponent } from 'src/app/pages/owner-orders/owner-orders.component';

const routes: Routes = [
  {path:'',component:OwnerOrdersComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerOrdersRoutingModule { }
