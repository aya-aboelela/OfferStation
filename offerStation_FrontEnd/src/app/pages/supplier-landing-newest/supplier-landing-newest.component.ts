import { Component } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';

@Component({
  selector: 'app-supplier-landing-newest',
  templateUrl: './supplier-landing-newest.component.html',
  styleUrls: ['./supplier-landing-newest.component.css']
})
export class SupplierLandingNewestComponent {
  categoryList!:any
  errorMessage: any;
  categoryName :any;
  offerList: any;
  constructor(private SupplierCategory:CategoryService,private route:ActivatedRoute, private Router:Router){
  }
  showAllOffers(catName:any){
    this.Router.navigate(['/owners/',catName]);
  }
  ngOnInit(): void {

    this.SupplierCategory.GetAllSupplierCategory().subscribe({
      next:data=>
      {
        let dataJson=JSON.parse(JSON.stringify(data))
        console.log(data);
        this.categoryList=dataJson.data;
        

      },
      error:error=>this.errorMessage=error

    })


   
}
}
