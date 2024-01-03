import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SuppliersOffersRoutingModule } from './suppliers-offers-routing.module';
import { Router, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
 
import { SupplierIndexComponent } from 'src/app/pages/supplier-index/supplier-index.component';
import { SupplierOffersPageComponent } from 'src/app/pages/supplier-offers-page/supplier-offers-page.component';
import { OffersFilterModule } from '../components-shared/offers-filter/offers-filter.module';
import { OwnersFiltersModule } from '../components-shared/owners-filters/owners-filters.module';
import { AllSupplierComponent } from 'src/app/pages/all-supplier/all-supplier.component';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatTabsModule} from '@angular/material/tabs';
import {HttpClientModule} from '@angular/common/http';
import { MatSliderModule } from '@angular/material/slider';
import { SellerCardModule } from '../components-shared/seller-card/seller-card.module';
import { ProductCardModule } from '../components-shared/product-card/product-card.module';
import { BannerModule } from '../components-shared/banner/banner.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    SupplierOffersPageComponent,
    
    AllSupplierComponent,
    SupplierIndexComponent,
     
    ],
  imports: [
    CommonModule,
    
    SuppliersOffersRoutingModule,
    NgxPaginationModule,
    MatSliderModule,
    MatTabsModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    OffersFilterModule,
    OwnersFiltersModule ,
    SellerCardModule  ,
    ProductCardModule,
    BannerModule,
     
  ]
})
export class SuppliersOffersModule { }
