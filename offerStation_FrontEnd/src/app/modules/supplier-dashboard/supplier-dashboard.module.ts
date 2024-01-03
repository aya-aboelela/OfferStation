import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { SupplierDashboardComponent } from 'src/app/pages/supplier-dashboard/supplier-dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { ChartModule } from '../chart/chart.module';

const routes: Routes = [
  { path: '', component: SupplierDashboardComponent },
];
@NgModule({
  declarations: [SupplierDashboardComponent],
  imports: [
    CommonModule,
    DataTablesModule,
    ChartModule,
    RouterModule.forChild(routes),
  ]
})
export class SupplierDashboardModule { }
