import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddressServiceService } from 'src/app/services/address/address';
import { city } from 'src/app/sharedClassesAndTypes/city';

@Component({
  selector: 'app-owner-offers-filter',
  templateUrl: './owner-offers-filter.component.html',
  styleUrls: ['./owner-offers-filter.component.css']
})
export class OwnerOffersFilterComponent implements OnInit {
  cities!: city[]
  selectedCityId: number = 0

  @Output() cityId: EventEmitter<number> = new EventEmitter<number>()


  constructor(private addressService: AddressServiceService) {

  }
  setIndex(selectedcity: number) {
    if (selectedcity != this.selectedCityId) {
      this.selectedCityId = selectedcity
      this.cityId.emit(selectedcity)
      console.log(this.selectedCityId)
    }

  }
  ngOnInit(): void {
    this.addressService.GetAllCities().subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        this.cities = dataJson.data
        console.log(this.selectedCityId)

      },
      error: error => { console.log(error) }
    }

    )
  }

}
