import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierProductOfferDetailsComponent } from 'src/app/components/supplier-product-offer-details/supplier-product-offer-details.component';


@NgModule({
  declarations: [SupplierProductOfferDetailsComponent],
  imports: [
    CommonModule
  ]
  ,exports:[
    SupplierProductOfferDetailsComponent
  ]
})
export class SupplierOfferProductDetailsModule { }
