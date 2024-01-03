import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OwnerRequestiedOrdersRoutingModule } from './owner-requestied-orders-routing.module';

import { OwnerRequestedOrdersComponent } from 'src/app/pages/owner-requested-orders/owner-requested-orders.component';
import { OwnerOfferProductDetailsModule } from '../components-shared/owner-offer-product-details/owner-offer-product-details.module';
import { DataTablesModule } from 'angular-datatables';
@NgModule({
  declarations: [OwnerRequestedOrdersComponent,   ],
  imports: [
    CommonModule,
    DataTablesModule,
    OwnerRequestiedOrdersRoutingModule ,
    OwnerOfferProductDetailsModule
  ],exports:[
    OwnerRequestedOrdersComponent
  ]
})
export class OwnerRequestiedOrdersModule { }
