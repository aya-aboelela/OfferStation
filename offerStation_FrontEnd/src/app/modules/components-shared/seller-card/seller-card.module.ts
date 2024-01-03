import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerCardComponent } from 'src/app/components/owner-card/owner-card.component';
import { BrowserModule } from '@angular/platform-browser';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [OwnerCardComponent],
  imports: [
    CommonModule,
    NgbModule
  ],
  exports:[
    OwnerCardComponent
  ]
})
export class SellerCardModule { }
