import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageService } from 'src/app/services/image.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';
import { Offer, OfferProduct } from 'src/app/sharedClassesAndTypes/OwnerOfferInfo';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-owner-offers',
  templateUrl: './owner-offers.component.html',
  styleUrls: ['./owner-offers.component.css']
})

export class OwnerOffersComponent implements OnInit {

  OfferList: any;
  ProductList: any;
  id: any;

  index!: any;
  imageUrl: string = '';
  errorMessage: any;
  productsTotalPrice: number = 0.0;

  display = '';
  display1 = '';

  ownerOffer: Offer = {
    ownerId: 0,
    createdTime: '',
    id: 0,
    prefPrice: 0,
    traderImage: '',
    name: '',
    description: '',
    price: 0,
    image: '',
    products: []
  }

  prods: OfferProduct[] = [];
  productDetails: Product = {
    id: 0,
    name: '',
    description: '',
    price: undefined,
    prefPrice: undefined,
    ownerId: 0,
    image: undefined,
    traderImage: undefined
  };
  OfferForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private _ownerService: OwnerService
    , private _imageService: ImageService,
    private activatedroute: ActivatedRoute) {

    this.OfferForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required]],
      image: [''],
      products: this.fb.array([]),
    });

    this.OfferForm.get('name')?.valueChanges.subscribe((data) => {
      this.ownerOffer.name = data;
    });
    this.OfferForm.get('description')?.valueChanges.subscribe((data) => {
      this.ownerOffer.description = data;
    });
    this.OfferForm.get('price')?.valueChanges.subscribe((data) => {
      this.ownerOffer.price = data;
    });
    this.OfferForm.get('image')?.valueChanges.subscribe((data) => {
      this.ownerOffer.image = data;
    });

  }



  ngOnInit(): void {

    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
    });
    this.LoadData()

    this._ownerService.getAllProductsByOwnerId(this.id).subscribe({
      next: data => {

        console.log(data);
        let dataJson = JSON.parse(JSON.stringify(data))
        this.ProductList = dataJson.data;

      },
      error: error => this.errorMessage = error
    });
  }

  LoadData() {
    this._ownerService.GetOffersByOwnerId(this.id).subscribe({
      next: data => {

        console.log(data);
        let dataJson = JSON.parse(JSON.stringify(data))
        this.OfferList = dataJson.data;
        this.OfferList.forEach((offer: Offer) => {
          offer.image = this._imageService.base64ArrayToImage(offer.image)
        });

      },
      error: error => this.errorMessage = error
    });
  }

  OnImageLoad(image: any) {
    this.imageUrl = this._imageService.base64ArrayToImage(image);
  }

  SubmitData() {

    const productsValue = this.OfferForm.get('products')?.value;
    const offerProducts: OfferProduct[] = productsValue?.map((product: any) => ({
      productId: product.productId,
      quantity: product.quantity,
    })) ?? [];

    const calculateProductsTotalPrice = async () => {
      for (const product of offerProducts) {
        const response = await this._ownerService.GetProductDetails(product.productId).toPromise();
        let dataJson = JSON.parse(JSON.stringify(response));
        let productDetails = dataJson.data;
        this.productsTotalPrice += (productDetails.price * product.quantity);
        console.log("total", this.productsTotalPrice);
      }

      if (this.OfferForm.get('price')?.value != null) {
        let offerPrice = parseInt(this.OfferForm?.get('price')?.value ?? '0');
        console.log("offerPrice", offerPrice);

        if (offerPrice && offerPrice > this.productsTotalPrice) {
          alert("cant be");
          return;
        }
        else {
          console.log("BeforeOwnerrrOferr", this.ownerOffer);
          this.ownerOffer.products = offerProducts;
          console.log("AfterOwnerrrOferr", this.ownerOffer);

          this._ownerService.AddOffer(this.id, this.ownerOffer).subscribe({
            next: data => {

              console.log(data);
              this.LoadData()
              this.onCloseOfferHandled();
              this.OfferForm.reset();
            },
            error: (error: any) => this.errorMessage = error,
          });
        }
      }
    }
    calculateProductsTotalPrice();
  }

  AddProduct() {
    this.products.push(this.CreateProduct());
  }

  CreateProduct() {
    return this.fb.group({
      quantity: ['', [Validators.required]],
      productId: ['', [Validators.required]],
    });
  }

  DeleteProduct(index: any) {
    this.products.removeAt(index);
  }

  DeleteOffer(offerId: number, index: number) {

    this._ownerService.DeleteOffer(offerId).subscribe({
      next: data => {
        this.OfferList.splice(index, 1);
        this.LoadData();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  public async ProcessFile(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = async (e: any) => {
        this.imageUrl = e.target.result;
        this.ownerOffer.image = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  }

  openOfferModal() {
    this.display = 'block';
    this.OfferForm.reset();
  }

  onCloseOfferHandled() {
    this.display = 'none';
    this.OfferForm.reset();
  }

  //Offer Form
  get name() {
    return this.OfferForm.get('name');
  }
  get description() {
    return this.OfferForm.get('description');
  }
  get price() {
    return this.OfferForm.get('price');
  }
  get image() {
    return this.OfferForm.get('image');
  }
  get products() {
    return this.OfferForm.get('products') as FormArray;
  }
  get productsControls() {
    return (this.OfferForm.get('products') as FormArray).controls as FormGroup[];
  }

}
