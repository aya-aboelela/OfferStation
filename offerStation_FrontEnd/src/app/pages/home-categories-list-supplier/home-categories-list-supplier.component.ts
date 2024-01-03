import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-home-categories-list-supplier',
  templateUrl: './home-categories-list-supplier.component.html',
  styleUrls: ['./home-categories-list-supplier.component.css']
})
export class HomeCategoriesListSupplierComponent implements OnInit{
  
  @Input() category:any

  @Input() SortBY:any
  offerList!: Product[];
  errorMessage: any;


 
  constructor(private SupplierCategory:CategoryService,private route:ActivatedRoute){
  }


  ngOnInit(): void {
    this.SupplierCategory.GetOffersWithSupplier(this.category,this.SortBY).subscribe({
      next:data=>
      {
        let dataJson=JSON.parse(JSON.stringify(data))
        console.log(dataJson);
        this.offerList=dataJson.data;
      },
      error:error=>this.errorMessage=error

    })
  }



}

  


