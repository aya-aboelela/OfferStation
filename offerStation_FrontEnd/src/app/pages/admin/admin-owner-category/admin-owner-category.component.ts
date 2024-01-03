import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { CategoryService } from 'src/app/services/Category/category.service';
import { AdminCategoriesService } from 'src/app/services/admin/admin-owner-categories.service';
import { ImageService } from 'src/app/services/image.service';
import { Category } from 'src/app/sharedClassesAndTypes/Category';
import { DataTableDirective } from 'angular-datatables';

@Component({
  selector: 'app-admin-owner-category',
  templateUrl: './admin-owner-category.component.html',
  styleUrls: ['./admin-owner-category.component.css']
})
export class AdminOwnerCategoryComponent {

  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;

  errorMessage: any;
  display = '';
  display1 = '';
  index!: any;
  imageUrl: string = '';

  categories: Category[] = [];

  ownerCategory: Category = {
    id: 0,
    name: '',
    image: ''
  };

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  categoryForm: FormGroup;

  constructor(
    private _categoryService: CategoryService,
    private _adminService: AdminCategoriesService,
    private _imageService: ImageService,
    private fb: FormBuilder,
  ) {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required]],
      image: ['']
    });

    this.categoryForm.get('name')?.valueChanges.subscribe((data) => {
      this.ownerCategory.name = data;
    });
    this.categoryForm.get('image')?.valueChanges.subscribe((data) => {
      this.ownerCategory.image = data;
    });
  }

  ngOnInit(): void {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [5, 10, 20],
      processing: true,
      destroy:true,
      responsive: true,
      language: {
        search: '_INPUT_',
        searchPlaceholder: 'Search records',
      }
    }
    this.getCategories();
  }

  getCategories(): void {
    this._categoryService.GetAllCategory()
      .subscribe(response => {
        this.categories = response.data
        this.dtTrigger.next(null);
        this.categories.forEach((category: Category) => {
          category.image = this._imageService.base64ArrayToImage(category.image)
        });
      });
  }

  public async ProcessFile(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = async (e: any) => {
        this.imageUrl = e.target.result;
        this.ownerCategory.image = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  }

  onDelete(divisionId: number, index: number) {
    this._adminService.DeleteCategory(divisionId).subscribe({
      next: data => {
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.categories.splice(index, 1);
        this.getCategories();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  OnSubmit() {
    this._adminService.AddCategory(this.categoryForm.value).subscribe({
      next: data => {
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.getCategories()
        this.onCloseCategoryHandled();
        this.categoryForm.reset();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  openEditCategoryModal(division: any, i: any) {
    this.display1 = 'block';
    this.index = i;
    this.ownerCategory.id = division.id
    console.log(division.image);
    this.categoryForm.patchValue(
      {
        name: division.name,
        image: division.image,//
      }
    )
  }

  onUpdate() {

    if(this.ownerCategory.image.includes('blob')){
      this.ownerCategory.image = '';
    }
    this._adminService.UpdateCategory(this.ownerCategory.id, this.ownerCategory).subscribe({
      next: data => {
        console.log("data",data);
        
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.getCategories()
        this.onCloseEditCategoryHandled();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  openCategoryModal() {
    this.display = 'block';
    this.categoryForm.reset();
  }


  onCloseCategoryHandled() {
    this.display = 'none';

  }

  onCloseEditCategoryHandled() {
    this.display1 = 'none';
  }

  get name() {
    return this.categoryForm.get('name');
  }
  get image() {
    return this.categoryForm.get('image');
  }
}
