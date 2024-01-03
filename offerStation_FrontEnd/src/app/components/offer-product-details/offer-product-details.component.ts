import { Component, Input, OnInit } from '@angular/core';
import { ImageService } from 'src/app/services/image.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { OfferProducts } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-offer-product-details',
  templateUrl: './offer-product-details.component.html',
  styleUrls: ['./offer-product-details.component.css']
})
export class OfferProductDetailsComponent  implements OnInit  
{

 
 product!:OfferProducts
 @Input() id:number=0
 @Input() Quantity:number=0
 @Input() Type:string=""
 constructor(private OwnerService:OwnerService,private _imageservice:ImageService) 
  {} 
 
  ngOnInit(): void {
  if(this.Type=="offer"){
    this.getOffers()

  }
  if(this.Type=="product"){
    this.getProduct
  }

 }


 getProduct(){
  
  this.OwnerService.GetProductDetails(this.id).subscribe((res) => {
    if (res.success) {
      let dataJson = JSON.parse(JSON.stringify(res))
      this.product=dataJson.data
      this.product.image=this._imageservice.base64ArrayToImage(this.product.image)


    } else {
      console.log(res.message); 
    }
  })

}
getOffers(){
  this.OwnerService.GetOfferdetails(this.id).subscribe((res) => {
    if (res.success) {
      let dataJson = JSON.parse(JSON.stringify(res))
      this.product=dataJson.data
      this.product.image=this._imageservice.base64ArrayToImage(this.product.image)


    } else {
      console.log(res.message); 
    }
  })

}
 
}
