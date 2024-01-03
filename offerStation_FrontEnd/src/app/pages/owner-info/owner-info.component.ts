import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OwnerprofileService } from 'src/app/services/OwnerProfile/ownerprofile.service';
import { ImageService } from 'src/app/services/image.service';
import { OwnerInfo } from 'src/app/sharedClassesAndTypes/OwnerInfo';

@Component({
  selector: 'app-owner-info',
  templateUrl: './owner-info.component.html',
  styleUrls: ['./owner-info.component.css']
})
export class OwnerInfoComponent implements OnInit {

  OwnerInfo: any;
  errorMessage: any;
  isUpdated: boolean = false;
  imageUrl = '';
  id: any;

  owner: OwnerInfo = {
    name: '',
    email: '',
    image: '',
    phoneNumber: ''
  };

  OwnerInfoForm: FormGroup;

  constructor(private fb: FormBuilder,
    private _ownerrServ: OwnerprofileService,
    private _imageService: ImageService,
    private activatedroute: ActivatedRoute) {

    this.OwnerInfoForm = this.fb.group({
      image: [''],
      name: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    });

    this.OwnerInfoForm.get('name')?.valueChanges.subscribe((data) => {
      this.owner.name = data;
    });
    this.OwnerInfoForm.get('phoneNumber')?.valueChanges.subscribe((data) => {
      this.owner.phoneNumber = data;
    });
    this.OwnerInfoForm.get('email')?.valueChanges.subscribe((data) => {
      this.owner.email = data;
    });
    this.OwnerInfoForm.get('image')?.valueChanges.subscribe((data) => {
      this.owner.image = data;
    });
  }

  ngOnInit(): void {

    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
    });

    this._ownerrServ.GetOwnerInfo(this.id).subscribe({
      next: (data: any) => {
       
        let dataJson = JSON.parse(JSON.stringify(data))
        this.owner = dataJson.data;
        this.imageUrl = this._imageService.base64ArrayToImage(this.owner.image);

        this.OwnerInfoForm.patchValue({
          image: this.imageUrl,
          name: this.owner.name,
          email: this.owner.email,
          phoneNumber: this.owner.phoneNumber
        })

      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  SubmitData() {

    if (window.confirm('Are you sure, you want to update?')) {
      this._ownerrServ.UpdateOwnerInfo(this.id, this.owner).subscribe({
        next: (data: any) => {
          console.log(data);
          this.OwnerInfo = data;
          this._ownerrServ.GetOwnerInfo(this.id);
          console.log(this.OwnerInfoForm.value);

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
        this.owner.image = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  }

  delete() {
    this.imageUrl = '';
  }

  //Owner Info Form

  get image() {
    return this.OwnerInfoForm.get('image');
  }
  get name() {
    return this.OwnerInfoForm.get('name');
  }
  get email() {
    return this.OwnerInfoForm.get('email');
  }
  get phoneNumber() {
    return this.OwnerInfoForm.get('phoneNumber');
  }
}
