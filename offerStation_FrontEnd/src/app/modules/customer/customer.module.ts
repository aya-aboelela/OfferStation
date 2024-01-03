import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerProfileComponent } from 'src/app/pages/customer-profile/customer-profile.component';
import { CustomerInfoComponent } from 'src/app/pages/customer-info/customer-info.component';
import { AddressesComponent } from 'src/app/pages/addresses/addresses.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [

  {
    path: 'profile', component: CustomerProfileComponent, children: [
      { path: 'adresses/:id', component: AddressesComponent },
      { path: 'Info/:id', component: CustomerInfoComponent },
      { path: 'customerOrders/:id', loadChildren: () => import('../customer-orders/customer-orders.module').then(mod => mod.CustomerOrdersModule) },

    ]
  },


];
@NgModule({
  declarations: [
    CustomerProfileComponent,
    CustomerInfoComponent,
    AddressesComponent,
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    RouterModule.forChild(routes),
    MatTabsModule,
    FormsModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
  ]
})
export class CustomerModule { }
