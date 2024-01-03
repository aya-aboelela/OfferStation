import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerRequestiedOrdersTabsRoutingModule } from './owner-requestied-orders-tabs-routing.module';
import {MatTabsModule} from '@angular/material/tabs';
import { OwnerRequestedOrdersTabsComponent } from 'src/app/pages/owner-requested-orders-tabs/owner-requested-orders-tabs.component';
import { OwnerRequestiedOrdersModule } from '../owner-requestied-orders/owner-requestied-orders.module';
import { OwnerRequestiedOrdersAfterShippedModule } from '../owner-requestied-orders-after-shipped/owner-requestied-orders-after-shipped.module';
@NgModule({
  declarations: [OwnerRequestedOrdersTabsComponent],
  imports: [
    CommonModule,
    OwnerRequestiedOrdersTabsRoutingModule,
    MatTabsModule,
    OwnerRequestiedOrdersModule,
    OwnerRequestiedOrdersAfterShippedModule

  ]
})
export class OwnerRequestiedOrdersTabsModule { }
