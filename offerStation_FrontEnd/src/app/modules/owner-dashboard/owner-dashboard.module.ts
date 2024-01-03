import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerDashboardComponent } from 'src/app/pages/owner-dashboard/owner-dashboard.component';

import { OwnerDashboardRoutingModule } from './owner-dashboard-routing.module';
import {DataTablesModule} from 'angular-datatables';
import { ChartModule } from '../chart/chart.module';
@NgModule({
  declarations: [OwnerDashboardComponent],
  imports: [
    CommonModule,
    OwnerDashboardRoutingModule,
    DataTablesModule,
    ChartModule
  ]
})
export class OwnerDashboardModule { }
