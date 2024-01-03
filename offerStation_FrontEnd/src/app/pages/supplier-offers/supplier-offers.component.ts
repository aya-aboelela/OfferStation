import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { SupplierService } from 'src/app/services/supplier/supplier.service';
import { Offer, OfferProduct } from 'src/app/sharedClassesAndTypes/OwnerOfferInfo';

@Component({
  selector: 'app-supplier-offers',
  templateUrl: './supplier-offers.component.html',
  styleUrls: ['./supplier-offers.component.css']
})

export class SupplierOffersComponent implements OnInit {

  OfferList: any;
  ProductList: any;

  offerProducts: OfferProduct[]=[];

  index!: any;
  imageUrl: string = '';
  errorMessage: any;

  display = '';
  display1 = '';

  productsTotalPrice: number = 0.0;
  id: any;

  supplierOffer: Offer = {
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
  OfferForm:FormGroup;
  constructor(private fb: FormBuilder,
    private _supplierService: SupplierService
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
        this.supplierOffer.name = data;
      });
      this.OfferForm.get('description')?.valueChanges.subscribe((data) => {
        this.supplierOffer.description = data;
      });
      this.OfferForm.get('price')?.valueChanges.subscribe((data) => {
        this.supplierOffer.price = data;
      });
      this.OfferForm.get('image')?.valueChanges.subscribe((data) => {
        this.supplierOffer.image = data;
      });
  }
  ngOnInit(): void {

    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
    });
    this.LoadData()

    this._supplierService.GetAllProductsBySupplierId(this.id).subscribe({
      next: data => {

        let dataJson = JSON.parse(JSON.stringify(data))
        this.ProductList = dataJson.data;
      },
      error: error => this.errorMessage = error
    });
  }

  LoadData() {
    this._supplierService.GetOffersBySupplierId(this.id).subscribe({
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

    console.log("Offerrr", this.supplierOffer);
    
    const productsValue = this.OfferForm.get('products')?.value;
    const offerProducts: OfferProduct[] = productsValue?.map((product: any) => ({
      productId: product.productId,
      quantity: product.quantity,
    })) ?? [];

    const calculateProductsTotalPrice = async () => {
      for (const product of offerProducts) {
        const response = await this._supplierService.GetProductDetails(product.productId).toPromise();
        let dataJson = JSON.parse(JSON.stringify(response));
        let productDetails = dataJson.data;
        this.productsTotalPrice += (productDetails.price * product.quantity);
      }

      if (this.OfferForm.get('price')?.value != null) {
        let offerPrice = parseInt(this.OfferForm?.get('price')?.value ?? '0');
        console.log("offerPrice", offerPrice);

        if (offerPrice && offerPrice > this.productsTotalPrice) {
          alert("cant be");
          return;
        }
        else {
          this._supplierService.AddOffer(this.id, this.supplierOffer).subscribe({
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

    this._supplierService.DeleteOffer(offerId).subscribe({
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
        this.supplierOffer.image = await this._imageService.imageToBase64Array(this.imageUrl);
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
