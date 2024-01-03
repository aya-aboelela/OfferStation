import { Component, Input, OnInit, Output } from '@angular/core';
import { CartService } from 'src/app/services/cart/cart.service';
import { ImageService } from 'src/app/services/image.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit{
  errorMessage: any;
  
  constructor(private cartService:CartService,private imageservice:ImageService){

  }
  ngOnInit(): void {
    this.sellerImage=this.imageservice.base64ArrayToImage(this.sellerImage)
    this.OfferImage=this.imageservice.base64ArrayToImage(this.OfferImage)

  }

  @Input() name:string=""
  @Input() id:any;

  @Input() offerId:number=0
  @Input() prefPrice:number=0
  @Input() afterPrice:number=0
  @Input() description:string=""
  @Input() SellerId:number=0
  @Input() Type:string=""
  @Input() sellerImage:any
  @Input() OfferImage:any
  hideElement:boolean=true
  display: string="none";
  offerList: any;
  product!:Product;
  openAddressModal() {
    this.display = 'block';
  }

  onCloseAddressHandled() {
    this.display = 'none';
  }
  AddToCustomerCart(){
    this.product={
     id:this.id,name:this.name, description:this.description,prefPrice:this.prefPrice,
     ownerId:this.SellerId,image:this.OfferImage,traderImage:this.OfferImage,price:this.afterPrice,}

   this.cartService.AddOfferToCart(this.product).subscribe({
     next: (data: any) => {
       console.log(data.data);
       this.offerList= data.data.offers;
       console.log(this.offerList)
     },
     error: (error: any) => this.errorMessage = error,
   });
 }
  AddToOwnerCart(){
    
  }
}
