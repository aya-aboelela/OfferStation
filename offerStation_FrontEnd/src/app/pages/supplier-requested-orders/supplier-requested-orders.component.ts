import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { OrdersOffer, OrdersProduct, RequestedOrders, orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-supplier-requested-orders',
  templateUrl: './supplier-requested-orders.component.html',
  styleUrls: ['./supplier-requested-orders.component.css']
})
export class SupplierRequestedOrdersComponent implements OnInit {
  @Input() SupplierId:number=1;
  ordertList:RequestedOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display:string="none"
  offersResult!:OrdersOffer[]
  productResult!:OrdersProduct[]
  orderStatus!:any
  orderId!: number;
  displayModel2: string="none";
 
  constructor(private OrderService:OrdersService,private route:ActivatedRoute,private router:Router) 
  {
    
  }
  changeStatus(orderId:number)
  {
    this.OrderService.OwnerOrderStatus(orderId,orderStatus.shipped).subscribe((res) => {
      if (res.success) {

        const currentUrl = this.router.url;
       this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });

      } else {
        console.log(res.message); 
      }
    })

  }
 
  
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OrderService.getSupplierOrdersRequested(this.SupplierId).subscribe((res) => {
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
