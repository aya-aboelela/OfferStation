import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierIndexComponent } from 'src/app/pages/supplier-index/supplier-index.component';

const routes: Routes = [{
  path: ':category',
  component: SupplierIndexComponent ,
}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuppliersOffersRoutingModule { }
