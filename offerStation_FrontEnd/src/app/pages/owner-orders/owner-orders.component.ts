import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { CustomerOrders, OrdersOffer, OrdersProduct, orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-owner-orders',
  templateUrl: './owner-orders.component.html',
  styleUrls: ['./owner-orders.component.css']
})
export class OwnerOrdersComponent implements OnInit {
  SupplierId:number=1;
  OwnerId:number=1;
  ordertList:CustomerOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display:string="none"
  offersResult!:OrdersOffer[]
  productResult!:OrdersProduct[]
  orderStatus!:any
  orderId!: number;
  displayModel2: string="none";

  
  openReviewsModal(SupplierId:number,orderId:number){
    this.SupplierId=SupplierId
    this.orderId=orderId
    this.displayModel2="block"

  }
  onCloseReviewHandled(){
    this.displayModel2="none"

  }
  closeModel(value: any) {
    this.displayModel2 = "none";
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });

    

  }
  constructor(private OrderService:OrdersService,private OwnerService:OwnerService,private route:ActivatedRoute,private router:Router) 
  {
    
  }
 
  ngOnInit(): void {
    this.OwnerId = this.route.snapshot.params['id']
    console.log(this.OwnerId)
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true
      
    };
    this.OrderService.GetOwnerOrders(this.OwnerId).subscribe((res) => {
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
  getOrderStatus(num:number){
     return orderStatus[num] 
  }
  onCloseAddressHandled() {
    this.display = 'none';
  }  

}
