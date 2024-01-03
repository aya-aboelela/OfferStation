import { Component, OnInit } from '@angular/core';
import { SupplierInfo } from 'src/app/sharedClassesAndTypes/SupplierInfo';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SupplierprofileService } from 'src/app/services/SupplierProfile/supplierprofile.service';
import { ImageService } from 'src/app/services/image.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-supplier-info',
  templateUrl: './supplier-info.component.html',
  styleUrls: ['./supplier-info.component.css']
})
export class SupplierInfoComponent implements OnInit {

  SupplierInfo: any;
  errorMessage: any;
  isUpdated: boolean = false;
  imageUrl = '';
  id: any;

  supplier: SupplierInfo = {
    name: '',
    email: '',
    image: '',
    phoneNumber: ''
  }

  SupplierInfoForm: FormGroup;

  constructor(private fb: FormBuilder,
    private _supplierServ: SupplierprofileService,
    private _imageService: ImageService,
    private activatedroute: ActivatedRoute) {

    this.SupplierInfoForm = this.fb.group({
      image: [''],
      name: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    });

    this.SupplierInfoForm.get('name')?.valueChanges.subscribe((data) => {
      this.supplier.name = data;
    });
    this.SupplierInfoForm.get('phoneNumber')?.valueChanges.subscribe((data) => {
      this.supplier.phoneNumber = data;
    });
    this.SupplierInfoForm.get('email')?.valueChanges.subscribe((data) => {
      this.supplier.email = data;
    });
    this.SupplierInfoForm.get('image')?.valueChanges.subscribe((data) => {
      this.supplier.image = data;
    });
  }

  ngOnInit(): void {

    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
    });
    console.log(this.id);


    this._supplierServ.GetSupplierById(this.id).subscribe({
      next: (data: any) => {
        let dataJson = JSON.parse(JSON.stringify(data))
        this.supplier = dataJson.data;
        this.imageUrl = this._imageService.base64ArrayToImage(this.supplier.image);
        this.SupplierInfoForm.patchValue({
          image: this.imageUrl,
          name: this.supplier.name,
          email: this.supplier.email,
          phoneNumber: this.supplier.phoneNumber
        })
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  SubmitData() {
    console.log(this.SupplierInfoForm.value);

    if (window.confirm('Are you sure, you want to update?')) {
      this._supplierServ.UpdateSupplierInfo(this.id, this.supplier).subscribe({
        next: (data: any) => {
          this.SupplierInfo = data;
          this._supplierServ.GetSupplierById(this.id);
        },
        error: (error: any) => this.errorMessage = error,
      });
    }
    this.isUpdated = !this.isUpdated;
  }

  public async ProcessFile(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = async (e: any) => {
        this.imageUrl = e.target.result;
        this.supplier.image = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  }

  delete() {
    this.imageUrl = '';
  }

  //Supplier Info Form

  get image() {
    return this.SupplierInfoForm.get('image');
  }
  get name() {
    return this.SupplierInfoForm.get('name');
  }
  get email() {
    return this.SupplierInfoForm.get('email');
  }
  get phoneNumber() {
    return this.SupplierInfoForm.get('phoneNumber');
  }

}
