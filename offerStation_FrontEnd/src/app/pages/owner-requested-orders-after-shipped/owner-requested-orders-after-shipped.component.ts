import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { OrdersOffer, OrdersProduct, RequestedOrders, orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-owner-requested-orders-after-shipped',
  templateUrl: './owner-requested-orders-after-shipped.component.html',
  styleUrls: ['./owner-requested-orders-after-shipped.component.css']
})
export class OwnerRequestedOrdersAfterShippedComponent implements OnInit {
  customerId:number=1;
  @Input() OwnerId:number=1;
  ordertList:RequestedOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display:string="none"
  offersResult!:OrdersOffer[]
  productResult!:OrdersProduct[]
  orderStatus!:any
  orderId!: number;
  displayModel2: string="none";
  getOrderStatus(num:number){
    return orderStatus[num] 
 }
  constructor(private OrderService:OrdersService) 
  {
    
  }
  
  
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OrderService.getOwnerOrdersRequestedAfterShipped(this.OwnerId).subscribe((res) => {
      if (res.success) {
        let dataJson = JSON.parse(JSON.stringify(res))
        this.ordertList=dataJson.data
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

  openAddressModal(offerlist:any,productList:any) {
    this.display = 'block';
    this.offersResult=offerlist
    this.productResult=productList
    
    
  }
 
  onCloseAddressHandled() {
    this.display = 'none';
  }  
}