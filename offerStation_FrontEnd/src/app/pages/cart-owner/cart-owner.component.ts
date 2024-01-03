import { Component } from '@angular/core';
import { CartService } from 'src/app/services/cart/cart.service';

@Component({
  selector: 'app-cart-owner',
  templateUrl: './cart-owner.component.html',
  styleUrls: ['./cart-owner.component.css']
})
export class CartOwnerComponent {
  display = '';
  productList:any;
   offerList:any;
   totalItem:any;
   totalPrice:any;
  errorMessage: any;
    constructor(private cartService:CartService){

  }
  ngOnInit(): void {
    this.cartService.GetCreateOwnerOrder().subscribe({
      next:data=>
      {
        console.log(data);
        this.offerList=data.data.offers;
        this.productList=data.data.products;
      },
      error:(error: any)=>this.errorMessage=error

    })

    this.cartService.GetOwnerCartdetails().subscribe({
      next:data=>
      {
        console.log(data);
        this.totalItem=data.data.itemsCount;
        this.totalPrice=data.data.total;

      },
      error:(error: any)=>this.errorMessage=error

    })
  }

  onDeleteProduct(ProductID:any){
    // this.display = 'none';
this.cartService.RemoveProductToOwnerCart(ProductID).subscribe({

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
