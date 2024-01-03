import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ActivatedRoute } from '@angular/router';
import { CustomerprofileService } from 'src/app/services/CustomerProfile/customerprofile-service.service';


import { Customer } from 'src/app/sharedClassesAndTypes/Customer';

@Component({
  selector: 'app-customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.css']
})

export class CustomerInfoComponent implements OnInit {

  CustomerInfo: any;
  id: any;
  errorMessage: any;
  isUpdated: boolean = false;

  customer: Customer = {
    name: '',
    phoneNumber: '',
    email: ''
  };

  CustomerInfoForm: any = this.fb.group({
    name: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]]
  });

  constructor(private fb: FormBuilder,
    private customerServ: CustomerprofileService,
    private activatedroute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });

    this.customerServ.GetCustomerById(this.id).subscribe({
      next: (data: any) => {

        let dataJson = JSON.parse(JSON.stringify(data))
        this.customer = dataJson.data;
        console.log(this.customer);

        this.CustomerInfoForm.patchValue({
          name: this.customer.name,
          phoneNumber: this.customer.phoneNumber,
          email: this.customer.email
        })
      },
      error: (error: any) => this.errorMessage = error,
    });

  }

  SubmitData() {

    if (window.confirm('Are you sure, you want to update?')) {
      this.customerServ.UpdateCustomerInfo(this.id, this.CustomerInfoForm.value).subscribe({
        next: (data: any) => {
          this.CustomerInfo = data;
          console.log(this.CustomerInfo);

        },
        error: (error: any) => this.errorMessage = error,
      });
    }
    this.isUpdated = !this.isUpdated;

  }

  //Customer Info Form
  get name() {
    return this.CustomerInfoForm.get('name');
  }
  get phoneNumber() {
    return this.CustomerInfoForm.get('phoneNumber');
  }
  get email() {
    return this.CustomerInfoForm.get('email');
  }

}
