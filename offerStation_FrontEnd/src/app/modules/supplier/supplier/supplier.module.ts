import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierRoutingModule } from './supplier-routing.module';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSliderModule } from '@angular/material/slider';
import { SupplierofferComponent } from 'src/app/pages/supplieroffers/supplieroffer/supplieroffer.component';
import { SupplierreviewsComponent } from 'src/app/pages/supplierreviews/supplierreviews/supplierreviews.component';
import { SupplierProfileComponent } from 'src/app/pages/supplier-profile/supplier-profile.component';
import { SupplierInfoComponent } from 'src/app/pages/supplier-info/supplier-info.component';
import { SupplierCategoriesComponent } from 'src/app/pages/supplier-categories/supplier-categories.component';
import { SupplierAddressesComponent } from 'src/app/pages/supplier-addresses/supplier-addresses.component';
import { SupplierProductsComponent } from 'src/app/pages/supplier-products/supplier-products.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SupplierOffersComponent } from 'src/app/pages/supplier-offers/supplier-offers.component';
import { SupplierproductComponent } from 'src/app/pages/supplierproduct/supplierproduct.component';
import { SuppliermainpageComponent } from 'src/app/pages/supplier-menu/suppliermainpage/suppliermainpage/suppliermainpage.component';
import { SupplieraddressesComponent } from 'src/app/pages/supplieraddresses/supplieraddresses/supplieraddresses.component';
import { MatIconModule } from '@angular/material/icon';


const routes: Routes = [
  { path: 'mainpage/:id', component: SuppliermainpageComponent },
  {
    path: 'profile', component: SupplierProfileComponent, children: [
      { path: 'adresses/:id', component: SupplierAddressesComponent },
      { path: 'Info/:id', component: SupplierInfoComponent },
      { path: 'categories/:id', component: SupplierCategoriesComponent },
      { path: 'products/:id', component: SupplierProductsComponent },
      { path: 'offers/:id', component: SupplierOffersComponent },
      { path: 'supplierDashboard/:id', loadChildren: () => import('../../supplier-dashboard/supplier-dashboard.module').then(mod => mod.SupplierDashboardModule) },
      { path: 'customerOrders/:id', loadChildren: () => import('../../supplier-requestied-orders-tabs/supplier-requestied-orders-tabs.module').then(mod => mod.SupplierRequestiedOrdersTabsModule) },

    ]
  },
]

@NgModule({
  declarations: [
    SupplierInfoComponent,
    SupplierproductComponent,
    SupplierofferComponent,
    SupplierreviewsComponent,
    SupplierProfileComponent,
    SuppliermainpageComponent,
    SupplierProductsComponent,
    SupplierCategoriesComponent,
    SupplierAddressesComponent,
    SupplieraddressesComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SupplierRoutingModule,
    MatTabsModule,
    MatSliderModule,
    NgxPaginationModule,
    NgbModule,
    MatSidenavModule,
    MatListModule,
    MatSliderModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule

  ]
})
export class SupplierModule { }
