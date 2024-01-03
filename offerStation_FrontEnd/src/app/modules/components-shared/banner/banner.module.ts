import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerComponent } from 'src/app/components/banner/banner.component';



@NgModule({
  declarations: [BannerComponent],
  exports:[
    BannerComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BannerModule { }
