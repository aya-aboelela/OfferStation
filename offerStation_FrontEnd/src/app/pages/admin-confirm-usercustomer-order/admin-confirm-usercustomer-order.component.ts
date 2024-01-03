import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { CustomerOrders, OrdersOffer, OrdersProduct, PendingOrders } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-admin-confirm-usercustomer-order',
  templateUrl: './admin-confirm-usercustomer-order.component.html',
  styleUrls: ['./admin-confirm-usercustomer-order.component.css']
})
export class AdminConfirmUsercustomerOrderComponent implements OnInit {
  
  orderId:number=0
  ordertList:PendingOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display:string="none"

  offersResult!:OrdersOffer[]
  productResult!:OrdersProduct[]
  orderStatus!:any

 
  closeModel(value: any) {
    this.display = "none";
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });

    

  } 
  constructor(private OrderService:OrdersService, private route:ActivatedRoute  ,private router:Router, private _userDataService: AuthenticationService
) 
  {}
 
  ngOnInit(): void {
    

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OrderService.getPendingCustomerOrders().subscribe((res) => {
      if (res.success) {
        let dataJson = JSON.parse(JSON.stringify(res))
        this.ordertList=dataJson.data
        console.log(this.ordertList)
        this.dtTrigger.next(null);

      } else {
        console.log(res.message); 
      }
    })
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.dtTrigger.unsubscribe();

  }

  openDelivaryModal(orderid:number){
    this.display = 'block';
    this.orderId=orderid
  }
  onCloseDeliveraryHandled() {
    this.display = 'none';
  }  
}
