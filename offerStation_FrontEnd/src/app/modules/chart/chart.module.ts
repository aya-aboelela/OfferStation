import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BarChartComponent } from 'src/app/components/bar-chart/bar-chart.component';


@NgModule({
  declarations: [BarChartComponent],
  
  exports:[
    BarChartComponent
  ]
})
export class ChartModule { }
