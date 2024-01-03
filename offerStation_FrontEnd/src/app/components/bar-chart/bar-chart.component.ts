import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AnalysisResult } from 'src/app/sharedClassesAndTypes/analysisResult';
import { ChartType } from 'chart.js';

Chart.register(...registerables)
@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent  implements OnInit{
  
  @Input() datalabels!:AnalysisResult[]
  @Input() label!:string
  @Input() ChartType: ChartType = "doughnut";

  @Input() canvaid!:string
  data:number[]=[]
  labels:string[]=[]

  constructor(){
   

  }
  ngOnChanges(changes: SimpleChanges) {
    console.log(changes)
    if(this.datalabels !=undefined){
      this.datalabels.forEach( (data) => {
        this.labels.push(data.name)
        this.data.push(data.count)
      })
      
      this.RenderChart(this.ChartType)

    }
   
  }
  ngOnInit(): void {
    
    }
   

    
  
  
  RenderChart(charttype:any){
    const myChart = new Chart(this.canvaid, {
      type: charttype,
      data: {
          labels: this.labels,
          datasets: [{
              label:this.label,
              data: this.data,
              backgroundColor: [
                  'rgba(255, 99, 132, 0.2)',
                  'rgba(54, 162, 235, 0.2)',
                  'rgba(255, 206, 86, 0.2)',
                  'rgba(75, 192, 192, 0.2)',
                  'rgba(153, 102, 255, 0.2)',
                  'rgba(255, 159, 64, 0.2)'
              ],
              borderColor: [
                  'rgba(255, 99, 132, 1)',
                  'rgba(54, 162, 235, 1)',
                  'rgba(255, 206, 86, 1)',
                  'rgba(75, 192, 192, 1)',
                  'rgba(153, 102, 255, 1)',
                  'rgba(255, 159, 64, 1)'
              ],
              borderWidth: 1
          }]
      },
      options: {
          scales: {
              y: {
                  beginAtZero: true
              }
          }
      }
  });
  }
    

}
