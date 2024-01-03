import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-owner-offers-page',
  templateUrl: './owner-offers-page.component.html',
  styleUrls: ['./owner-offers-page.component.css']
})
export class ownerOffersPageComponent implements OnInit {

  ProductList!: Product[]
  selectedcityId: number = 0
  pageNumber: number = 1
  totalItems: number = 0
  pagesize: number = 6
  sortBy: string = ""
  OwnerCategory: string = "Clothes"
  display :string="none"

  constructor(private OwnerService: OwnerService, private route: ActivatedRoute) {
  }

  cityIdChanges(value: any) {
    this.selectedcityId = value;
    this.getproduct(this.pageNumber, this.pagesize, this.OwnerCategory, this.selectedcityId, this.sortBy)
    this.pageNumber = 1

  }
  ngOnInit(): void {
    this.OwnerCategory = this.route.snapshot.params['category']
    this.getproduct(1, this.pagesize, this.OwnerCategory, this.selectedcityId, this.sortBy)
  }

  getproduct(pgNum: number, pageSize: number, ownerCategory: string, cityId: number, sortingBy: string) {
    console.log(ownerCategory)
    this.OwnerService.GetOwnerOffer(pgNum, pageSize, ownerCategory, cityId, sortingBy).subscribe({
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
    this.getproduct(this.pageNumber, this.pagesize, this.OwnerCategory, this.selectedcityId, this.sortBy)
    this.pageNumber = 1
    console.log(value);

  }
  changeSorting(selectObject: any) {
    this.sortBy = selectObject.target.value
    this.getproduct(this.pageNumber, this.pagesize, this.OwnerCategory, this.selectedcityId, this.sortBy)


  }
  openAddressModal() {
    this.display = 'block';
  }

  onCloseAddressHandled() {
    this.display = 'none';
  }

}
