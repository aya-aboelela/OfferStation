import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { PendingOrders } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-admin-confirm-ownercustomer-order',
  templateUrl: './admin-confirm-ownercustomer-order.component.html',
  styleUrls: ['./admin-confirm-ownercustomer-order.component.css']
})
export class AdminConfirmOwnercustomerOrderComponent implements OnInit {
  
  orderId:number=0
  ordertList:PendingOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display:string="none"


  orderStatus!:any

  closeModel(value: any) {
    this.display = "none";
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });

    

  } 

  constructor(private OrderService:OrdersService, private route:ActivatedRoute  , private _userDataService: AuthenticationService,private router:Router
) 
  {}
 
  ngOnInit(): void {
    

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OrderService.getPendingOwnerOrders().subscribe((res) => {
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