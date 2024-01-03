import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SupplierService } from 'src/app/services/supplier/supplier.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-supplier-offers-page',
  templateUrl: './supplier-offers-page.component.html',
  styleUrls: ['./supplier-offers-page.component.css']
})
export class SupplierOffersPageComponent {
  
  ProductList!: Product[]
  selectedcityId: number = 0
  pageNumber: number = 1
  totalItems: number = 0
  pagesize: number = 4
  sortBy: string = ""
  SupplierCategory: string = "Clothes"

  constructor(private supplierService: SupplierService, private route: ActivatedRoute) {
  }

  cityIdChanges(value: any) {
    this.selectedcityId = value;
    this.getproduct(this.pageNumber, this.pagesize, this.SupplierCategory, this.selectedcityId, this.sortBy)
    this.pageNumber = 1

  }
  ngOnInit(): void {
    this.SupplierCategory = this.route.snapshot.params['category']
    this.getproduct(1, this.pagesize, this.SupplierCategory, this.selectedcityId, this.sortBy)
  }

  getproduct(pgNum: number, pageSize: number, ownerCategory: string, cityId: number, sortingBy: string) {
    console.log(ownerCategory)
    this.supplierService.GetSupplierOffer(pgNum, pageSize, ownerCategory, cityId, sortingBy).subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        console.log(dataJson.data)
        this.totalItems = dataJson.data.itemsCount
        this.ProductList = dataJson.data.list

      },
      error: error => { console.log(error) }
    }

    )
  }

  pageNumberChanged(value: any) {
    this.pageNumber = value
    this.getproduct(this.pageNumber, this.pagesize, this.SupplierCategory, this.selectedcityId, this.sortBy)
    this.pageNumber = 1
    console.log(value);

  }
  changeSorting(selectObject: any) {
    this.sortBy = selectObject.target.value
    this.getproduct(this.pageNumber, this.pagesize, this.SupplierCategory, this.selectedcityId, this.sortBy)


  }

}
