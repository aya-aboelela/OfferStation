import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SupplierService } from 'src/app/services/supplier/supplier.service';
import { Seller } from 'src/app/sharedClassesAndTypes/Owner';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-all-supplier',
  templateUrl: './all-supplier.component.html',
  styleUrls: ['./all-supplier.component.css']
})
export class AllSupplierComponent {
  SupplierList!: Seller[]
  selectedcityId: number = 0
  pageNumber: number = 1
  totalItems: number = 0
  pagesize: number = 4
  sortBy: string = ""
  SupplierCategory: string = "Clothes"
  ownerName: string = ""

  constructor(private SupplierService: SupplierService, private route: ActivatedRoute) {

  }

  cityIdChanges(value: any) {
    this.selectedcityId = value;
    this.getSuppliers(this.pageNumber, this.pagesize)
    this.pageNumber = 1


  }
  ngOnInit(): void {
    this.SupplierCategory = this.route.snapshot.params['category']
    this.getSuppliers(1, this.pagesize)
  }

  getSuppliers(pgNum: number, pageSize: number) {
    this.SupplierService.GetSuppliers(pgNum, pageSize, this.SupplierCategory, this.selectedcityId, this.sortBy, this.ownerName).subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        console.log(dataJson.data)
        this.totalItems = dataJson.data.itemsCount
        this.SupplierList = dataJson.data.list

      },
      error: error => { console.log(error) }
    }

    )
  }
  pageNumberChanged(value: any) {
    this.pageNumber = value
    this.getSuppliers(this.pageNumber, this.pagesize)
    this.pageNumber = 1
    console.log(value);

  }
  changeSorting(selectObject: any) {
    this.sortBy = selectObject.target.value
    this.getSuppliers(this.pageNumber, this.pagesize)


  }
  ownersearchanges(value: any) {
    this.ownerName = value;
    this.getSuppliers(this.pageNumber, this.pagesize)
    this.pageNumber = 1

  }
}
