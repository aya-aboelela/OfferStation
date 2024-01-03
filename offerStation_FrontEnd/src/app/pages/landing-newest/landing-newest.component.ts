import { CategoryService } from './../../services/Category/category.service';
import { ownerCategory } from './../../sharedClassesAndTypes/ownerCategory';
import { Owner } from 'src/app/sharedClassesAndTypes/Owner';
import { Component } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { ownerCategoryWithOffers } from 'src/app/sharedClassesAndTypes/ownerCategoryWithOffers';

@Component({
  selector: 'app-landing-newest',
  templateUrl: './landing-newest.component.html',
  styleUrls: ['./landing-newest.component.css']
})
export class LandingNewestComponent {
  categoryList!:any
  errorMessage: any;
  categoryName :any;
  offerList: any;
  constructor(private ownerCategory:CategoryService,private route:ActivatedRoute, private Router:Router){
  }
  showAllOffers(catName:any){
    this.Router.navigate(['/owners/',catName]);
  }
  ngOnInit(): void {

    this.ownerCategory.GetAllCategory().subscribe({
      next:data=>
      {
        this.categoryList=data.data;
       
      },
      error:error=>this.errorMessage=error

    })
}
AddToCart(offer:any){

}
}
