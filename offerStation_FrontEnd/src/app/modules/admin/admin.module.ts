import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from 'src/app/pages/admin/admin-main/admin.component';
import { AdminOwnerCategoryComponent } from 'src/app/pages/admin/admin-owner-category/admin-owner-category.component';
import { AdminReviewsComponent } from 'src/app/pages/admin/admin-reviews/admin-reviews.component';
import { AdminOwnerReviewsComponent } from 'src/app/pages/admin/admin-owner-reviews/admin-owner-reviews.component';
import { AdminSupplierCategoryComponent } from 'src/app/pages/admin/admin-supplier-category/admin-supplier-category.component';
import { AdminWaitingOwnersComponent } from 'src/app/pages/admin/admin-waiting-owners/admin-waiting-owners.component';
import { AdminWaitingSuppliersComponent } from 'src/app/pages/admin/admin-waiting-suppliers/admin-waiting-suppliers.component';
import { AdminDeliveryComponent } from 'src/app/pages/admin/admin-delivery/admin-delivery.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatTabsModule } from '@angular/material/tabs';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { DataTablesModule } from 'angular-datatables';

@NgModule({
  declarations: [
    AdminComponent,
    AdminOwnerCategoryComponent,
    AdminSupplierCategoryComponent,
    AdminReviewsComponent,
    AdminOwnerReviewsComponent,
    AdminWaitingOwnersComponent,
    AdminWaitingSuppliersComponent,
    AdminDeliveryComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MatTabsModule,
    MatSidenavModule,
    MatListModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatIconModule,
    MatDialogModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers:[]
})
export class AdminModule {}
