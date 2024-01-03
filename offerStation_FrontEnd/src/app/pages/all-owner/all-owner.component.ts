import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { Seller } from 'src/app/sharedClassesAndTypes/Owner';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-all-owner',
  templateUrl: './all-owner.component.html',
  styleUrls: ['./all-owner.component.css']
})
export class AllOwnerComponent implements OnInit {
  ownerList!: Seller[]
  selectedcityId: number = 0
  pageNumber: number = 1
  totalItems: number = 0
  pagesize: number = 4
  sortBy: string = ""
  OwnerCategory: string = "Clothes"
  ownerName: string = ""

  constructor(private OwnerService: OwnerService, private route: ActivatedRoute) {

  }

  cityIdChanges(value: any) {
    this.selectedcityId = value;
    this.getOwners(this.pageNumber, this.pagesize)
    this.pageNumber = 1


  }
  ngOnInit(): void {
    this.OwnerCategory = this.route.snapshot.params['category']
    this.getOwners(1, this.pagesize)
  }

  getOwners(pgNum: number, pageSize: number) {
    this.OwnerService.GetOwners(pgNum, pageSize, this.OwnerCategory, this.selectedcityId, this.sortBy, this.ownerName).subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        console.log(dataJson.data)
        this.totalItems = dataJson.data.itemsCount
        this.ownerList = dataJson.data.list

      },
      error: error => { console.log(error) }
    }

    )
  }
  pageNumberChanged(value: any) {
    this.pageNumber = value
    this.getOwners(this.pageNumber, this.pagesize)
    this.pageNumber = 1
    console.log(value);

  }
  changeSorting(selectObject: any) {
    this.sortBy = selectObject.target.value
    this.getOwners(this.pageNumber, this.pagesize)


  }
  ownersearchanges(value: any) {
    this.ownerName = value;
    this.getOwners(this.pageNumber, this.pagesize)
    this.pageNumber = 1

  }

}
