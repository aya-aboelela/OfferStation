import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/services/cart/cart.service';
import { ImageService } from 'src/app/services/image.service';
import { SupplierService } from 'src/app/services/supplier/supplier.service';

@Component({
  selector: 'app-supplieroffer',
  templateUrl: './supplieroffer.component.html',
  styleUrls: ['./supplieroffer.component.css']
})
export class SupplierofferComponent implements OnInit{
  supplieroffers:any;
  id:any;
  pageNumber:number=1
  pagesize:number=3
  ProductListofOffer:any

PageNumberChanged(value: number) {
  this.pageNumber = value


}
  errorMessage: any;
  constructor(private cartService:CartService,private supplier:SupplierService, private activatedroute: ActivatedRoute,private imageService: ImageService)
  {

  }

  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });
 this.supplier.GetOffersBySupplierId(this.id).subscribe({

    next: (data: { data: any; }) => {
      console.log(data);
      this.supplieroffers = data.data
      this.applayImages()
    },
    error: (error: any) => this.errorMessage = error

  });

  }
  display = '';
  openModal(id:number) {
    this.display = 'block';
    console.log(id);
    this.supplier.GetofferDetails(id).subscribe({
      next: (data: any) => {
        console.log(data);
        this.ProductListofOffer = data.data;
        this.ProductListofOffer=this.ProductListofOffer.foreach((product:any)=>{
          product.image=this.imageService.base64ArrayToImage(product.image) })
      },
      error: (error: any) => this.errorMessage = error,
    })
  }
  applayImages(){
    for(let product of this.ProductListofOffer ){
      product.image=this.imageService.base64ArrayToImage(product.image)
    }
    // this.supplieroffers=this.supplieroffers.foreach((product:any)=>{
    //    product.image=this.imageService.base64ArrayToImage(product.image)
    // });
  }
  closeModal() {
    this.display = 'none';
  }

  AddToCart(Product:any)
  {
    this.cartService.AddOfferToOwnerCart(Product).subscribe({
      error: error => this.errorMessage = error
    });
  }


}
