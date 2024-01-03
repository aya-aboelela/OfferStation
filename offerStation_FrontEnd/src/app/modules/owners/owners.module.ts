
import { Router, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnersRoutingModule } from './owners-routing.module';
import { ownerOffersPageComponent } from 'src/app/pages/owner-offers-page/owner-offers-page.component';
import { AllOwnerComponent } from 'src/app/pages/all-owner/all-owner.component';
import { OwnersIndexComponent } from 'src/app/pages/owners-index/owners-index.component';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatTabsModule} from '@angular/material/tabs';
import {HttpClientModule} from '@angular/common/http';
import { MatSliderModule } from '@angular/material/slider';
import { SellerCardModule } from '../components-shared/seller-card/seller-card.module';
import { OwnersFiltersModule } from '../components-shared/owners-filters/owners-filters.module';
import { OffersFilterModule } from '../components-shared/offers-filter/offers-filter.module';
import { BannerModule } from '../components-shared/banner/banner.module';
import { ProductCardModule } from '../components-shared/product-card/product-card.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  declarations: [
    ownerOffersPageComponent,
    AllOwnerComponent,
    OwnersIndexComponent,
  
  
  ],
  imports: [
    NgxPaginationModule,
    MatSliderModule,
    MatTabsModule,
    CommonModule,
    HttpClientModule,
    OwnersRoutingModule,
    RouterModule,
    FormsModule,
    OwnersFiltersModule,
    OffersFilterModule,
    SellerCardModule,
    ProductCardModule,
    BannerModule,
    NgbModule
 
    
  ]
})
export class OwnersModule { }
