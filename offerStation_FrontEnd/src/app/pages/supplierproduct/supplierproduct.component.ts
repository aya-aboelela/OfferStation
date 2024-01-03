import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/services/cart/cart.service';
import { ImageService } from 'src/app/services/image.service';
import { SupplierService } from 'src/app/services/supplier/supplier.service';

@Component({
  selector: 'app-supplierproduct',
  templateUrl: './supplierproduct.component.html',
  styleUrls: ['./supplierproduct.component.css']
})
export class SupplierproductComponent  implements OnInit{
  pageNumber:number=1
  pagesize:number=1
  selectedValue=0
  min=0;
  max=500;
  id:any
  allProductsBySupplierID:any;
  MenucategoryList: any;
  ProductListByCategoryName: any
  errorMessage: any;
  selectedId: number | undefined;
  constructor(private cartService:CartService,private supplier: SupplierService,private activatedroute:ActivatedRoute, private imageService: ImageService) {
  }
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });
    this.supplier.GetMenuCategoiesBySupplierId(this.id).subscribe({

      next: (data: { data: any; }) => {
        console.log(data);
        this.MenucategoryList = data.data

      },
      error: (error: any) => this.errorMessage = error

    });
    this.supplier.GetMinPriceoFProductBySupplierID(this.id).subscribe({

      next: data => {
        console.log(data);
        this.min = data.data;
        console.log("min "+this.min)
      },
      error: error => this.errorMessage = error

    });
    this.supplier.GetMaxPriceoFProductBySupplierID(this.id).subscribe({

      next: data => {
        console.log(data);
        this.max = data.data;
        console.log("max "+this.max)
      },
      error: error => this.errorMessage = error

    });
    this.getAllProductsBySupplierId()

    }
    setIndex(selectedId:number){
      this.selectedId=selectedId
      this.getproductBycategoryId(selectedId)
    }

    getAllProductsBySupplierId()
    {
    this.supplier.GetAllProductsBySupplierId(this.id).subscribe({

      next: (data: { data: any; }) => {
        console.log(data);
        this.ProductListByCategoryName = data.data
        console.log(this.ProductListByCategoryName);
        this.applayImages()

      },
      error: (error: any) => this.errorMessage = error

    });

    this.pageNumber = 1

}

getproductBycategoryId(value:number)
{
  if (value== 0) {
   this.getAllProductsBySupplierId();
  }
  else {
    this.supplier.GetAllProductsByMenuCategoryID(value).subscribe({
      next: (data: { data: any; }) => {
        console.log(data);
        this.ProductListByCategoryName = data.data
        console.log("list" + this.ProductListByCategoryName);

      },
      error: (error: any) => this.errorMessage = error

    })
  }
}

applayImages(){

  for(let product of this.ProductListByCategoryName ){
    product.image=this.imageService.base64ArrayToImage(product.image)
  }
}

getselectedprice(value:number)
{

  this.ProductListByCategoryName=this.ProductListByCategoryName.filter((product:any)=>{
    return product.price<=value
  });
  this.pageNumber = 1


}
PageNumberChanged(value:any){
    this.pageNumber=value

}
AddToCart(Product:any)
{
  this.cartService.AddProductToOwnerCart(Product).subscribe({
    error: error => this.errorMessage = error
  });
}
}


