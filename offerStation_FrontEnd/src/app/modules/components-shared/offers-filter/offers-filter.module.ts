import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerOffersFilterComponent } from 'src/app/components/owner-offers-filter/owner-offers-filter.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser'



@NgModule({
  declarations: [OwnerOffersFilterComponent
  ],
  imports:[
    FormsModule,
   CommonModule
  ],
  exports:[
    OwnerOffersFilterComponent
  ]
})
export class OffersFilterModule { }
