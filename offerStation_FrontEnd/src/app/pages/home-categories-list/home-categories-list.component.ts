import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
import { Product } from 'src/app/sharedClassesAndTypes/product';

@Component({
  selector: 'app-home-categories-list',
  templateUrl: './home-categories-list.component.html',
  styleUrls: ['./home-categories-list.component.css']
})
export class HomeCategoriesListComponent  implements OnInit{
  
  @Input() category:any

  @Input() SortBY:any
  offerList!: Product[];
  errorMessage: any;


  constructor(private ownerCategory:CategoryService,private route:ActivatedRoute){
  }


  ngOnInit(): void {
    this.ownerCategory.GetOffersWithOwner(this.category,this.SortBY).subscribe({
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
