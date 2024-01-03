import { Offer } from 'src/app/sharedClassesAndTypes/OwnerOfferInfo';
import { Product } from 'src/app/sharedClassesAndTypes/product';
import { CartService } from './../../services/cart/cart.service';
import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-cart-user',
  templateUrl: './cart-user.component.html',
  styleUrls: ['./cart-user.component.css']
})
export class CartUserComponent implements OnInit{
  display = '';
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

  makeorder(){
    this.cartService.MakeOrder().subscribe({
      next:data=>
      {
        console.log(data);  

      },
      error:(error: any)=>this.errorMessage=error
    })
  }
  onDeleteProduct(ProductID:any){
    // this.display = 'none';
this.cartService.RemoveProductToCart(ProductID).subscribe({

})
  }
  Sub(value:any){
   let newvalue=value--
  //  document.getElementById("quantity").value=newvalue;
  }
  Add(value:any){
  value++
  }

}
