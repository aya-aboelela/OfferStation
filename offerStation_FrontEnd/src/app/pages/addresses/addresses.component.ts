import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AddressServiceService } from 'src/app/services/address/address';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { AddressDetails } from 'src/app/sharedClassesAndTypes/AddressDetails';
import { city } from 'src/app/sharedClassesAndTypes/city';

@Component({
  selector: 'app-addresses',
  templateUrl: './addresses.component.html',
  styleUrls: ['./addresses.component.css'],
})
export class AddressesComponent implements OnInit {

  ApplicationuserId: any;

  errorMessage: any;
  display = '';
  display1 = '';
  id: any;
  AddressList: any;

  Address: AddressDetails = {
    details: '',
    cityId: 0,
    id: 0,
    cityName: ''
  };

  cities!: city[]
  city!: city

  addressForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _addressService: AddressServiceService,
    private _cityService: AddressServiceService,
    private _userDataService: AuthenticationService,
    private activatedroute: ActivatedRoute
  ) {
    this.addressForm = this.fb.group({
      details: [''],
      cityId: [''],
    })

    this.addressForm.get('details')?.valueChanges.subscribe((data) => {
      this.Address.details = data;
    });

    this.addressForm.get('cityId')?.valueChanges.subscribe((data) => {
      this.Address.cityId = data;
    });

  }

  ngOnInit(): void {

    // this.ApplicationuserId = this._userDataService.userData;
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
      this.ApplicationuserId = paramMap.get('id');
    });

    this.LoadData();

    this._cityService.GetAllCities().subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        this.cities = dataJson.data
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  LoadData() {
    this._addressService.GetUserAdresses(this.ApplicationuserId).subscribe({//this.ApplicationuserId._value.nameid
      next: data => {
        console.log(data);
        let dataJson = JSON.parse(JSON.stringify(data))
        this.AddressList = dataJson.data;
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  SubmitData() {

    this._addressService.AddAddress(this.ApplicationuserId, this.addressForm.value).subscribe({ 
      next: data => {
        this.LoadData()
        this.onCloseAddressHandled();
        this.addressForm.reset();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  DeleteAddress(addressId: number, index: number) {

    this._addressService.DeleteAddress(addressId).subscribe({
      next: data => {
        this.AddressList.splice(index, 1);
        this.LoadData();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  UpdateAddress() {
    console.log(this.addressForm.value);
    this._addressService.UpdateAddress(this.Address.id, this.Address).subscribe({
      next: data => {
        this.LoadData();
        this.onCloseEditAddressHandled();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  opeEditAddressModal(addressId: number) {
    this.display1 = 'block';
    this._addressService.GetAdressesDetails(addressId).subscribe({
      next: data => {
        let dataJson = JSON.parse(JSON.stringify(data))
        this.Address = dataJson.data;
      },
      error: (error: any) => this.errorMessage = error,
    });

  }

  openAddressModal() {
    this.display = 'block';
  }

  onCloseAddressHandled() {
    this.display = 'none';
  }

  onCloseEditAddressHandled() {
    this.display1 = 'none';
  }


  //Address Form
  get details() {
    return this.addressForm.get('details');
  }
  get cityId() {
    return this.addressForm.get('cityId');
  }

}
