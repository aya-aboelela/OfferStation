import { OwnerofferdetailComponent } from './../../pages/owner-offer-details/ownerofferdetail/ownerofferdetail.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { OwnerProductComponent } from 'src/app/pages/owner-product/owner-product/owner-product.component';
import { MatTabsModule } from '@angular/material/tabs';
import { OwnerMenuComponent } from 'src/app/pages/menu/owner-menu/owner-menu.component';
import { MatSliderModule } from '@angular/material/slider';
import { OwnerreviewComponent } from 'src/app/pages/ownerreview/ownerreview.component';
import { NgbModule, } from '@ng-bootstrap/ng-bootstrap';
import { OwnerProfileComponent } from 'src/app/pages/owner-profile/owner-profile.component';
import { OwnerInfoComponent } from 'src/app/pages/owner-info/owner-info.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OwnerProductsComponent } from 'src/app/pages/owner-products/owner-products.component';
import { OwnerCategoriesComponent } from 'src/app/pages/owner-categories/owner-categories.component';
import { OwnerAddressesComponent } from 'src/app/pages/owner-addresses/owner-addresses.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { OwnerdetailsComponent } from 'src/app/pages/owner-details/ownerdetails/ownerdetails.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { OwnerOffersComponent } from 'src/app/pages/owner-offers/owner-offers.component';
import { MatIconModule } from '@angular/material/icon';




const routes: Routes = [
  { path: 'mainpage/:id', component: OwnerProductComponent },
  {
    path: 'profile', component: OwnerProfileComponent, children: [
      { path: 'ownerDashboard/:id', loadChildren: () => import('../owner-dashboard/owner-dashboard.module').then(mod => mod.OwnerDashboardModule) },
      { path: 'branches/:id', component: OwnerAddressesComponent },
      { path: 'Info/:id', component: OwnerInfoComponent },
      { path: 'categories/:id', component: OwnerCategoriesComponent },
      { path: 'products/:id', component: OwnerProductsComponent },
      { path: 'offers/:id', component: OwnerOffersComponent },
      { path: 'ownerOrders/:id', loadChildren: () => import('../owner-orders/owner-orders.module').then(mod => mod.OwnerOrdersModule) },
      { path: 'CustomerOrders/:id', loadChildren: () => import('../owner-requestied-orders-tabs/owner-requestied-orders-tabs.module').then(mod => mod.OwnerRequestiedOrdersTabsModule) },

    ]
  },

];

@NgModule({
  declarations: [
    OwnerProductComponent,
    OwnerMenuComponent,
    OwnerreviewComponent,
    OwnerInfoComponent,
    OwnerProfileComponent,
    OwnerAddressesComponent,
    OwnerCategoriesComponent,
    OwnerProductsComponent,
    OwnerofferdetailComponent,
    OwnerdetailsComponent


  ],
  imports: [
    CommonModule,
    NgbModule,
    NgxPaginationModule,
    MatSliderModule,
    MatTabsModule,
    FormsModule,
    MatIconModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatSidenavModule,
    MatListModule
  ]
})
export class OwnerModule { }
