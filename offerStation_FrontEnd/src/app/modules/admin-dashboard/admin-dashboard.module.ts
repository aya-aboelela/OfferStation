import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DataTablesModule} from 'angular-datatables';

import { ChartModule } from '../chart/chart.module';
import { AdminDashboardRoutingModule } from './admin-dashboard-routing.module';
import { AdminDashboardComponent } from 'src/app/pages/admin-dashboard/admin-dashboard.component';

@NgModule({
  declarations: [AdminDashboardComponent],
  imports: [
    DataTablesModule,
    ChartModule,
    CommonModule,
    AdminDashboardRoutingModule
  ]
})
export class AdminDashboardModule { }
