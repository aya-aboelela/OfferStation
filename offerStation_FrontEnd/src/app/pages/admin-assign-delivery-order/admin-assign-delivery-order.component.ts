import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { delivery } from 'src/app/sharedClassesAndTypes/delivery';
import { orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-admin-assign-delivery-order',
  templateUrl: './admin-assign-delivery-order.component.html',
  styleUrls: ['./admin-assign-delivery-order.component.css']
})
export class AdminAssignDeliveryOrderComponent implements OnInit {
  
  deliveryList!:delivery[]
  deliveryId:number=0
  @Input() Type!:string
  @Input() OrderId!:number
  @Output() modelClose: EventEmitter<number> = new EventEmitter<number>()


  constructor(private OrderService:OrdersService, private route:ActivatedRoute ,private router:Router , private _userDataService: AuthenticationService) 
  {}

  ngOnInit(): void {
    this.OrderService.GetAllDeliveres().subscribe((res) => {
      if (res.success) {
        let dataJson = JSON.parse(JSON.stringify(res))
        this.deliveryList=dataJson.data
        console.log(this.deliveryList)

      } else {
        console.log(res.message); 
      }
    })
  
    
    
  }
  selectChange(selectObject: any) {
    this.deliveryId = selectObject.target.value
  }
  ConfirmCustomerOrder(){
    this.OrderService.postCustomerOrderDelivary(this.OrderId,this.deliveryId).subscribe((res) => {
      if (res.success) {
        this.OrderService.CustomerOrderStatus(this.OrderId,orderStatus.ordered).subscribe((res) => {
          if (res.success) {
            this.modelClose.emit(1)

    
          } else {
            console.log(res.message); 
          }
        })

      } else {
        console.log(res.message); 
      }
    })

  }
  ConfirmOwnerOrder(){
    this.OrderService.postOwnerOrderDelivary(this.OrderId,this.deliveryId).subscribe((res) => {
      if (res.success) {
        this.OrderService.OwnerOrderStatus(this.OrderId,orderStatus.ordered).subscribe((res) => {
          if (res.success) {
            console.log("success")
            this.router.navigate(['admin/ownerOrders'])
    
          } else {
            console.log(res.message); 
          }
        })

      } else {
        console.log(res.message); 
      }
    })

  }

}
