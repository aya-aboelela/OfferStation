import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCardComponent } from 'src/app/components/product-card/product-card.component';

import { OfferProductsComponent } from 'src/app/components/offer-products/offer-products.component';
@NgModule({
  declarations: [ProductCardComponent ,OfferProductsComponent],
  imports: [
    CommonModule
  ],
  exports:[
    ProductCardComponent
  ]
})
export class ProductCardModule { }
