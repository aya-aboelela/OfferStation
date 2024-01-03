import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { CustomerOrders, OrdersOffer, OrdersProduct, orderStatus } from 'src/app/sharedClassesAndTypes/order';
import { OfferProducts } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  orderId!:number
  ownerId!:number;
  customerId!:number;
  ordertList:CustomerOrders[]=[]
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  display: string = "none"
  displayModel2: string = "none"

  offersResult!: OrdersOffer[]
  productResult!: OrdersProduct[]
  orderStatus!: any

  openReviewsModal(OwnerId: number, orderId: number) {
    this.ownerId = OwnerId
    this.orderId = orderId
    this.displayModel2 = "block"

  }
  onCloseReviewHandled() {
    this.displayModel2 = "none"

  }

  constructor(private OrderService:OrdersService,private OwnerService:OwnerService, private router:Router,private route:ActivatedRoute  , private _userDataService: AuthenticationService
) 
  {}
 
  ngOnInit(): void {

    this.customerId = this.route.snapshot.params['id']

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [5, 10, 20],
      processing: true

    };

    
    this.OrderService.GetCustomerOrders(this.customerId).subscribe((res) => {
      if (res.success) {
        let dataJson = JSON.parse(JSON.stringify(res))
        this.ordertList = dataJson.data
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

  closeModel(value: any) {
    this.displayModel2 = "none";
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });

    

  }

  openAddressModal(offerlist:any,productList:any) {
    this.display = 'block';
    this.offersResult = offerlist
    this.productResult = productList


  }
  getOrderStatus(num: number) {
    return orderStatus[num]
  }
  onCloseAddressHandled() {
    this.display = 'none';
  }
}
