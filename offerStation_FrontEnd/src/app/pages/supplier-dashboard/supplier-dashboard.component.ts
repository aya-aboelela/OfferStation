import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { AnalysisResult, customerResult } from 'src/app/sharedClassesAndTypes/analysisResult';
import { SupplierDashboardService } from 'src/app/services/supplier/supplier-dashboard/supplier-dashboard.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-supplier-dashboard',
  templateUrl: './supplier-dashboard.component.html',
  styleUrls: ['./supplier-dashboard.component.css']
})
export class SupplierDashboardComponent implements OnDestroy, OnInit{

  TopCustomerData:customerResult[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor( private _SupplierAnalysisServ: SupplierDashboardService,private route: ActivatedRoute) { }

  totalCustomers:number=0
  totalOrders:number=0;
  totalProfits:number=0;
  totalProducts:number=0
  totalOffers:number=0;
  supplierId:number=1
  topOffers!:AnalysisResult[]
  topProduct!:AnalysisResult[]
  orderStatus!:AnalysisResult[]
  offersProductsOrdersCount!:AnalysisResult[]

  
  
  ngOnInit(): void {
    this.supplierId = this.route.snapshot.params['id']

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    
  this.getTopoffers(this.supplierId) 
  this.getTopProducts(this.supplierId)
  this.getOrdersStatus(this.supplierId)
  this.getOffersProductOrdersCount(this.supplierId)
  this.GetCustomersCount(this.supplierId)
  this.GetOrdersCount(this.supplierId)
  this.GetProductsCount(this.supplierId)
  this.GetTotalProfits(this.supplierId)
  this.GetOffersCount(this.supplierId)
  this.getTopCustomersOrder(this.supplierId)
  
  







  }
  
  getTopCustomersOrder(supplierId:number){
    this._SupplierAnalysisServ.GetTopOrderdCustomers(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.TopCustomerData=dataJson.data
        this.dtTrigger.next(null);

       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getOffersProductOrdersCount(supplierId:number){
    this._SupplierAnalysisServ.GetOffersProductsCount(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.offersProductsOrdersCount=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }

  getOrdersStatus(supplierId:number){
    this._SupplierAnalysisServ.GetOrdersStatus(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.orderStatus=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getTopoffers(supplierId:number){
    this._SupplierAnalysisServ.GetTopOffers(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.topOffers=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  getTopProducts(supplierId:number){
    this._SupplierAnalysisServ.GetTopProduct(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.topProduct=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )

  }
  GetCustomersCount(supplierId:number){
  
    this._SupplierAnalysisServ.GetTotalCustomers(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalCustomers=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
  }
  GetOrdersCount(supplierId:number){
  
    this._SupplierAnalysisServ.GetTotalOrders(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOrders=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetTotalProfits(supplierId:number){
  
    this._SupplierAnalysisServ.GetTotalProfit(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProfits=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }

  GetProductsCount(supplierId:number){
  
    this._SupplierAnalysisServ.GetProductCount(supplierId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProducts=dataJson.data
       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  
  GetOffersCount(supplierId:number){
  
    this._SupplierAnalysisServ.GetOffersCount(supplierId).subscribe({
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
