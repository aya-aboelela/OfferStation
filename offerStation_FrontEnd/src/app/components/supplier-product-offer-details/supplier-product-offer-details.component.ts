import { Component, Input, OnInit } from '@angular/core';
import { ImageService } from 'src/app/services/image.service';
import { SupplierService } from 'src/app/services/supplier/supplier.service';
import { OfferProducts } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-supplier-product-offer-details',
  templateUrl: './supplier-product-offer-details.component.html',
  styleUrls: ['./supplier-product-offer-details.component.css']
})
export class SupplierProductOfferDetailsComponent implements OnInit  
{

 
 product!:OfferProducts
 @Input() id:number=0
 @Input() Quantity:number=0
 @Input() Type:string=""
 constructor(private SupplierService:SupplierService ,private _imageservice:ImageService) 
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
  
  this.SupplierService.GetProductDetails(this.id).subscribe((res) => {
    if (res.success) {
      let dataJson = JSON.parse(JSON.stringify(res))
      this.product=dataJson.data
      this.product.image=this._imageservice.base64ArrayToImage(this.product.image)
      console.log(this.product)

    } else {
      console.log(res.message); 
    }
  })

}
getOffers(){
  this.SupplierService.GetofferDetails(this.id).subscribe((res) => {
    if (res.success) {
      let dataJson = JSON.parse(JSON.stringify(res))
      this.product=dataJson.data
      this.product.image=this._imageservice.base64ArrayToImage(this.product.image)
      console.log(this.product)

    } else {
      console.log(res.message); 
    }
  })

}
 
}