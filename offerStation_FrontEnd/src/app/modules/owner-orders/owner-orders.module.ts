import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerOrdersRoutingModule } from './owner-orders-routing.module';
import { OwnerOrdersComponent } from 'src/app/pages/owner-orders/owner-orders.component';
import { DataTablesModule } from 'angular-datatables';
import { OwnerReviewComponent } from 'src/app/pages/owner-review/owner-review.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { SupplierOfferProductDetailsModule } from '../components-shared/supplier-offer-product-details/supplier-offer-product-details.module';
@NgModule({
  declarations: [OwnerOrdersComponent,OwnerReviewComponent],
  imports: [
    CommonModule,
    DataTablesModule,
    OwnerOrdersRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    SupplierOfferProductDetailsModule
  ]
})
export class OwnerOrdersModule { }
