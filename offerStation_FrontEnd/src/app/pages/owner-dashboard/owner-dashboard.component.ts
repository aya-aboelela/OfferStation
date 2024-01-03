import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { OwnerAnalysisService } from 'src/app/services/owner/owner-analysis/owner-analysis.service';
import { AnalysisResult, customerResult } from 'src/app/sharedClassesAndTypes/analysisResult';

@Component({
  selector: 'app-owner-dashboard',
  templateUrl: './owner-dashboard.component.html',
  styleUrls: ['./owner-dashboard.component.css']
})
export class OwnerDashboardComponent implements OnDestroy, OnInit{

  TopCustomerData:customerResult[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor( private _ownerAnalysisServ: OwnerAnalysisService,private route:ActivatedRoute) { }

  totalCustomers:number=0
  totalOrders:number=0;
  totalProfits:number=0;
  totalProducts:number=0
  totalOffers:number=0;
  ownerId!:number
  topOffers!:AnalysisResult[]
  topProduct!:AnalysisResult[]
  orderStatus!:AnalysisResult[]
  offersProductsOrdersCount!:AnalysisResult[]

  
  
  ngOnInit(): void {
    this.ownerId = this.route.snapshot.params['id']

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    
  this.getTopoffers(this.ownerId) 
  this.getTopProducts(this.ownerId)
  this.getOrdersStatus(this.ownerId)
  this.getOffersProductOrdersCount(this.ownerId)
  this.GetCustomersCount(this.ownerId)
  this.GetOrdersCount(this.ownerId)
  this.GetProductsCount(this.ownerId)
  this.GetTotalProfits(this.ownerId)
  this.GetOffersCount(this.ownerId)
  this.getTopCustomersOrder(this.ownerId)
  
  







  }
  
  getTopCustomersOrder(ownerId:number){
    this._ownerAnalysisServ.GetTopOrderdCustomers(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.TopCustomerData=dataJson.data
        this.dtTrigger.next(null);

       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getOffersProductOrdersCount(ownerId:number){
    this._ownerAnalysisServ.GetOffersProductsCount(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.offersProductsOrdersCount=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }

  getOrdersStatus(ownerId:number){
    this._ownerAnalysisServ.GetOrdersStatus(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.orderStatus=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getTopoffers(ownerId:number){
    this._ownerAnalysisServ.GetTopOffers(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.topOffers=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getTopProducts(ownerId:number){
    this._ownerAnalysisServ.GetTopProduct(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.topProduct=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  GetCustomersCount(ownerId:number){
  
    this._ownerAnalysisServ.GetTotalCustomers(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalCustomers=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
  }
  GetOrdersCount(ownerId:number){
  
    this._ownerAnalysisServ.GetTotalOrders(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOrders=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetTotalProfits(ownerId:number){
  
    this._ownerAnalysisServ.GetTotalProfit(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProfits=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }

  GetProductsCount(ownerId:number){
  
    this._ownerAnalysisServ.GetProductCount(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProducts=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  
  GetOffersCount(ownerId:number){
  
    this._ownerAnalysisServ.GetOffersCount(ownerId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOffers=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }



}
