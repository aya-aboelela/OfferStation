import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart/cart.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  productList:any;
  offerList:any;
  totalItem:any;
  totalPrice:any;
 errorMessage: any;
   constructor(private cartService:CartService){

 }
  ngOnInit(): void {
    this.cartService.GetCartdetails().subscribe({
      next:data=>
      {
        console.log(data);
        this.offerList=data.data.offers;
        this.productList=data.data.products;
      },
      error:(error: any)=>this.errorMessage=error

    })
    
    this.cartService.GetCreateOrder().subscribe({
      next:data=>
      {
        console.log(data);
        this.totalItem=data.data.itemsCount;
        this.totalPrice=data.data.total;
      },
      error:(error: any)=>this.errorMessage=error

    })
  }
}
