import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { AdminDashboardService } from 'src/app/services/admin/admin-dashboard/admin-dashboard.service';
import { OwnerAnalysisService } from 'src/app/services/owner/owner-analysis/owner-analysis.service';
import { AnalysisResult, customerResult } from 'src/app/sharedClassesAndTypes/analysisResult';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {

  TopCustomerData:customerResult[]=[]
  OwnertdtOptions: DataTables.Settings = {};
  OwnerdtTrigger: Subject<any> = new Subject<any>();
  TopOwnerData:customerResult[]=[]

  CustomerdtOptions: DataTables.Settings = {};
  CustomerdtTrigger: Subject<any> = new Subject<any>();

  constructor( private _adminAnalysisServ: AdminDashboardService) { }

  totalCustomers:number=0
  totalOwners:number=0
  totalSuppliers:number=0
  totalOrders:number=0;
  totalProfits:number=0;
  totalProducts:number=0
  totalOffers:number=0;
  ownerId:number=1
  ordersOffers!:AnalysisResult[]
  ordersProduct!:AnalysisResult[]
  

  
  
  ngOnInit(): void {
    this.CustomerdtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OwnertdtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.GetOrderedOffers()
    this.GetOrderedProducts()
    this.GetProfits()
    this.GetTotalCustomers()
    this.GetTotalOffers()
    this.GetTotalOrderedCustomer()
    this.GetTotalOrderedOwners()
    this.GetTotalOrders()
    this.GetTotalOwners()
    this.GetTotalProduct()
    this.GetTotalSuppliers()
    
  
  
  







  }
  GetTotalCustomers() {
    this._adminAnalysisServ.GetTotalCustomers().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalCustomers=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
    
  }
  GetTotalOwners(){
    this._adminAnalysisServ.GetTotalOwners().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOwners=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetTotalSuppliers() {
    this._adminAnalysisServ.GetTotalSuppliers().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalSuppliers=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetTotalOrders() {
    this._adminAnalysisServ.GetTotalOrders().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOrders=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )    
  }
  GetTotalProduct() {
    this._adminAnalysisServ.GetTotalProduct().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProducts=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetTotalOffers (){
    this._adminAnalysisServ.GetTotalOffers().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalOffers=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
  }
  GetProfits() {
    this._adminAnalysisServ.GetProfits().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.totalProfits=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )
    
  }
  GetOrderedOffers() {
    this._adminAnalysisServ.GetOrderedOffers().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.ordersOffers=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )    
  }
  GetOrderedProducts() {
    this._adminAnalysisServ.GetOrderedProducts().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.ordersProduct=dataJson.data

       
      },
      error:error=>{console.log(error)}
    }
      
      )    
  }
  GetTotalOrderedCustomer() {
    this._adminAnalysisServ.GetTotalOrderedCustomer().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.TopCustomerData=dataJson.data
        this.CustomerdtTrigger.next(null);

       
      },
      error:error=>{console.log(error)}
    }
      
      )    
  }
  GetTotalOrderedOwners() {
    this._adminAnalysisServ.GetTotalOrderedOwners().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.TopOwnerData=dataJson.data
        this.OwnerdtTrigger.next(null);


       
      },
      error:error=>{console.log(error)}
    }
      
      )    
  }
  
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.CustomerdtTrigger.unsubscribe();
    this.OwnerdtTrigger.unsubscribe();

  }
}
