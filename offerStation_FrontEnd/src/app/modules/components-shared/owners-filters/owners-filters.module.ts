import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerFilterComponent } from 'src/app/components/owner-filter/owner-filter.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser'

@NgModule({
  declarations: [OwnerFilterComponent],
  imports:[FormsModule,CommonModule],
  
  exports:[
    OwnerFilterComponent
  ]
})
export class OwnersFiltersModule { }
