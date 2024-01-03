import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerOrdersRoutingModule } from './customer-orders-routing.module';
import { CustomerOrdersComponent } from 'src/app/pages/customer-orders/customer-orders.component';
import {DataTablesModule} from 'angular-datatables';
import { CustomerReviewComponent } from 'src/app/pages/customer-review/customer-review.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { OwnerOfferProductDetailsModule } from '../components-shared/owner-offer-product-details/owner-offer-product-details.module';
@NgModule({
  declarations: [CustomerOrdersComponent,CustomerReviewComponent], 
  imports: [
    CommonModule,
    CustomerOrdersRoutingModule,
    DataTablesModule,
    NgbModule,
    ReactiveFormsModule,
    OwnerOfferProductDetailsModule
  ]
})
export class CustomerOrdersModule { }
