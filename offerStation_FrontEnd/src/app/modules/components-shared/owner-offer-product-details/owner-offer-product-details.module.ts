import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfferProductDetailsComponent } from 'src/app/components/offer-product-details/offer-product-details.component';



@NgModule({
  declarations: [OfferProductDetailsComponent],
  imports: [
    CommonModule
  ],
  exports:[OfferProductDetailsComponent]

   
})
export class OwnerOfferProductDetailsModule { }
