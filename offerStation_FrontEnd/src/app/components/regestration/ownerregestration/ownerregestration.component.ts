import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
// import { OwnerCategoryServiceService } from 'src/app/services/OwnerCategory/owner-category-service.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { CityService } from 'src/app/services/city/city.service';
import { PhoneValidator } from 'src/app/validators/PhoneValidator.validation';
import { ConfirmPasswordValidator } from 'src/app/validators/confirmPassword.validation';

@Component({
  selector: 'app-ownerregestration',
  templateUrl: './ownerregestration.component.html',
  styleUrls: ['./ownerregestration.component.css']
})
export class OwnerregestrationComponent {

  constructor(private _AuthService: AuthenticationService,
    private fb: FormBuilder,
    private cityService: CityService,
  private category:CategoryService,
  private router: Router
  ) {

  }
  msg='';
  Cities: any;
  categories: any;
  error: string = ''
  registerForm = this.fb.group({
    Name: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required,PhoneValidator()]],
    Address: this.fb.array([]),
    Password: ['', [Validators.required,Validators.maxLength(10),Validators.minLength(6)]],
    ownerCategoryId:['',[Validators.required]],
    Confirm: ['', [Validators.required]],
    Email: ['', [Validators.required, Validators.email]],
  }, { validator: [ConfirmPasswordValidator] })

  get Name() {
    return this.registerForm.get('Name');
  }
  
  get phoneNumber() {
    return this.registerForm.get('phoneNumber');
  }
  get Address() {
    return this.registerForm.get('Address') as FormArray;
  }
  get AddressControls() {
    return (this.registerForm.get('Address') as FormArray).controls as FormGroup[];
  }
  get Password() {
    return this.registerForm.get('Password');
  }
  get Confirm() {
    return this.registerForm.get('Confirm');
  }
  get Email() {
    return this.registerForm.get('Email');
  }


  ngOnInit() {
    this.cityService.GetAllCities().subscribe({
      next: data => this.Cities = data.data,
      error: error => console.log(error)
    });
    this.category.GetAllCategory().subscribe({
      next: data => this.categories = data.data,
      error: error => console.log(error)
    });

  }

  addAddress() {
    this.Address.push(this.createMaterial());
  }
  createMaterial() {
    return this.fb.group({
      details: ['', [Validators.required]],
      CityId: ['', [Validators.required]],
    });
  }
  deleteAddress(index: any) {
    this.Address.removeAt(index);
  }

  submitData() {
    this._AuthService.registerOwner(this.registerForm.value).subscribe({
      next:data=>{  
        console.log(this.registerForm.value);    
        if(data.success)
          this.router.navigate(['login']);
        else
          this.error = data.data;
      },
      error: error => console.log(error)
    })
  }


  removemesage(){
    this.msg="";
  }
}
