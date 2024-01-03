import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { ProductDetails, ProductInfo } from 'src/app/sharedClassesAndTypes/ProductInfo';
import { ownerCategory } from 'src/app/sharedClassesAndTypes/ownerCategory';

@Component({
  selector: 'app-owner-products',
  templateUrl: './owner-products.component.html',
  styleUrls: ['./owner-products.component.css']
})
export class OwnerProductsComponent implements OnInit {

  errorMessage: any;
  ProductList: any
  index!: any;
  id: any;
  imageUrl: string = '';

  display = '';
  display1 = '';

  ownerProduct: ProductInfo = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    image: '',
    categoryId: 0,
    discount: 0,
    discountPrice: 0
  }

  categories!: ownerCategory[]
  category!: ownerCategory

  productForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _ownerService: OwnerService,
    private _imageService: ImageService,
    private activatedroute: ActivatedRoute) {

    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required]],
      discount: ['', [Validators.required]],
      discountPrice: [''],
      image: [''],
      categoryId: ['', [Validators.required]],
    });

    this.productForm.get('name')?.valueChanges.subscribe((data) => {
      this.ownerProduct.name = data;
    });
    this.productForm.get('description')?.valueChanges.subscribe((data) => {
      this.ownerProduct.description = data;
    });
    this.productForm.get('price')?.valueChanges.subscribe((data) => {
      this.ownerProduct.price = data;
    });
    this.productForm.get('discount')?.valueChanges.subscribe((data) => {
      this.ownerProduct.discount = data;
    });
    this.productForm.get('image')?.valueChanges.subscribe((data) => {
      this.ownerProduct.image = data;
    });
    this.productForm.get('categoryId')?.valueChanges.subscribe((data) => {
      this.ownerProduct.categoryId = data;
    });
  }

  ngOnInit(): void {

    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));
    });

    this.LoadData();

    this._ownerService.getMenuCategorybyOwnerId(this.id).subscribe({
      next: data => {

        let dataJson = JSON.parse(JSON.stringify(data))
        this.categories = dataJson.data;
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  LoadData() {
    this._ownerService.getAllProductsByOwnerId(this.id).subscribe({
      next: data => {
        console.log(data);
        let dataJson = JSON.parse(JSON.stringify(data))
        this.ProductList = dataJson.data;
        this.ProductList.forEach((product: ProductInfo) => {
          product.image = this._imageService.base64ArrayToImage(product.image)
        });

      },
      error: error => this.errorMessage = error
    });
  }

  SubmitData() {

    console.log(this.productForm.value);
    this._ownerService.AddProduct(this.id, this.ownerProduct).subscribe({
      next: data => {
        console.log(data);
        this.LoadData()
        this.onCloseProductHandled();
        this.productForm.reset();
      },
      error: (error: any) => this.errorMessage = error,
    });

  }

  DeleteProduct(productId: number, index: number) {

    this._ownerService.DeleteProduct(productId).subscribe({
      next: data => {
        this.ProductList.splice(index, 1);
        this.LoadData();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  UpdateProduct() {
    this._ownerService.UpdateProduct(this.ownerProduct.id, this.ownerProduct).subscribe({
      next: data => {
        this.ProductList[this.index] = this.productForm.value;
        this.LoadData();
        this.onCloseEditProductHandled();
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
        this.ownerProduct.image = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  }

  openEditProductModal(product: any, i: any) {
    this.display1 = 'block';
    this.index = i;
    console.log(product)
    this.ownerProduct.id = product.id
    this.productForm.patchValue(
      {
        name: product.name,
        categoryId: product.categoryId,
        discount: product.discount,
        discountPrice: product.discountPrice,
        price: product.price,
        description: product.description,
        image: product.image
      }
    )

  }

  openProductModal() {
    this.display = 'block';
    this.productForm.reset();
  }

  onCloseProductHandled() {
    this.display = 'none';
  }

  onCloseEditProductHandled() {
    this.display1 = 'none';
  }

  //Product Form
  get name() {
    return this.productForm.get('name');
  }
  get description() {
    return this.productForm.get('description');
  }
  get price() {
    return this.productForm.get('price');
  }
  get discount() {
    return this.productForm.get('discount');
  }
  get discountPrice() {
    return this.productForm.get('discountPrice');
  }
  get image() {
    return this.productForm.get('image');
  }
  get categoryId() {
    return this.productForm.get('categoryId');
  }

}
