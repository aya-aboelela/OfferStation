import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierRequestiedOrdersRoutingModule } from './supplier-requestied-orders-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { SupplierRequestedOrdersComponent } from 'src/app/pages/supplier-requested-orders/supplier-requested-orders.component';
import { SupplierOfferProductDetailsModule } from '../components-shared/supplier-offer-product-details/supplier-offer-product-details.module';


@NgModule({
  declarations: [SupplierRequestedOrdersComponent],
  imports: [
    CommonModule,
    DataTablesModule, 
    SupplierOfferProductDetailsModule,
    SupplierRequestiedOrdersRoutingModule,
    

  ],
  exports:[
    SupplierRequestedOrdersComponent
  ]
})
export class SupplierRequestiedOrdersModule { }
