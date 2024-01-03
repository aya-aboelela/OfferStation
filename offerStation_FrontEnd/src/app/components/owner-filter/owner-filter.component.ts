import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddressServiceService } from 'src/app/services/address/address';
import { city } from 'src/app/sharedClassesAndTypes/city';


@Component({
  selector: 'app-owner-filter',
  templateUrl: './owner-filter.component.html',
  styleUrls: ['./owner-filter.component.css']
})
export class OwnerFilterComponent {
  cities!:city[]
  selectedCityId:number=0
  OwnerNameInput:string=""
  @Output() cityId:EventEmitter<number>=new EventEmitter<number>()
  @Output() OwnerName:EventEmitter<string>=new EventEmitter<string>()


  constructor(private addressService:AddressServiceService){}
  setIndex(selectedcity:number){
    if(selectedcity!=this.selectedCityId){
      this.selectedCityId=selectedcity
      this.cityId.emit(selectedcity)
    }
  
  }
  searchByName(name:string){
    this.OwnerName.emit(name);
  }
  ngOnInit(): void {
    this.addressService.GetAllCities().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.cities=dataJson.data
        console.log(this.selectedCityId)

      },
      error:error=>{console.log(error)}
    }
      
      )
  }

}
