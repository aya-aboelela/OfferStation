import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OwnerRequestiedOrdersAfterShippedRoutingModule } from './owner-requestied-orders-after-shipped-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { OwnerOfferProductDetailsModule } from '../components-shared/owner-offer-product-details/owner-offer-product-details.module';
import { OwnerRequestedOrdersAfterShippedComponent } from 'src/app/pages/owner-requested-orders-after-shipped/owner-requested-orders-after-shipped.component';


@NgModule({
  declarations: [OwnerRequestedOrdersAfterShippedComponent],
  imports: [
    CommonModule,
    OwnerRequestiedOrdersAfterShippedRoutingModule,
    DataTablesModule,
    OwnerOfferProductDetailsModule
  ],
  exports:[
    OwnerRequestedOrdersAfterShippedComponent
  ]
})
export class OwnerRequestiedOrdersAfterShippedModule { }
