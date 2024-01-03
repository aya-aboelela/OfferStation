import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';

@Component({
  selector: 'app-landing-best-seller',
  templateUrl: './landing-best-seller.component.html',
  styleUrls: ['./landing-best-seller.component.css']
})
export class LandingBestSellerComponent {
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
        //let dataJson=JSON.parse(JSON.stringify(data))
        console.log(data);
        this.categoryList=data.data;
        
      },
      error:error=>this.errorMessage=error

    })
   

  }
}
