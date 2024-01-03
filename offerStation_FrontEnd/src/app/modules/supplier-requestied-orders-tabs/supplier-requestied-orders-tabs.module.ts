import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SupplierRequestiedOrdersTabsRoutingModule } from './supplier-requestied-orders-tabs-routing.module';
import { SupplierRequestedOrdersTabsComponent } from 'src/app/pages/supplier-requested-orders-tabs/supplier-requested-orders-tabs.component';
import { SupplierRequestiedOrdersAfterShippedModule } from '../supplier-requestied-orders-after-shipped/supplier-requestied-orders-after-shipped.module';
import { SupplierRequestiedOrdersModule } from '../supplier-requestied-orders/supplier-requestied-orders.module';
import { MatTabsModule } from '@angular/material/tabs';


@NgModule({
  declarations: [SupplierRequestedOrdersTabsComponent],
  imports: [
    MatTabsModule,
    CommonModule,
    SupplierRequestiedOrdersAfterShippedModule,
    SupplierRequestiedOrdersModule,
    
    SupplierRequestiedOrdersTabsRoutingModule

  ]
})
export class SupplierRequestiedOrdersTabsModule { }
