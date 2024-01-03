import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-supplier-header',
  templateUrl: './supplier-header.component.html',
  styleUrls: ['./supplier-header.component.css']
})
export class SupplierHeaderComponent  implements OnInit {
  categoryList: any;
  errorMessage: any;
 
  //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
  //Add 'implements OnInit' to the class.
  
  constructor(private supplierCategory:CategoryService,private route:ActivatedRoute ,private imageservice:ImageService){
  }
  
  ngOnInit(): void {

    this.supplierCategory.GetAllSupplierCategory().subscribe({
      next:data=>
      {
        this.categoryList=data.data;
       
      },
      error:error=>this.errorMessage=error

    })
  }
}
