import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SupplierRequestiedOrdersAfterShippedRoutingModule } from './supplier-requestied-orders-after-shipped-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { SupplierOfferProductDetailsModule } from '../components-shared/supplier-offer-product-details/supplier-offer-product-details.module';
import { SupplierRequestedOrdersAfterShippedComponent } from 'src/app/pages/supplier-requested-orders-after-shipped/supplier-requested-orders-after-shipped.component';


@NgModule({
  declarations: [SupplierRequestedOrdersAfterShippedComponent],
  imports: [
    CommonModule,
    DataTablesModule,
    SupplierOfferProductDetailsModule,
    SupplierRequestiedOrdersAfterShippedRoutingModule
  ],
  exports:[
    SupplierRequestedOrdersAfterShippedComponent
  ]
})
export class SupplierRequestiedOrdersAfterShippedModule { }
