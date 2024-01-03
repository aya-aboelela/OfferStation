import { Component,NO_ERRORS_SCHEMA, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SupplierService } from 'src/app/services/supplier/supplier.service';

@Component({
  selector: 'app-supplierreviews',
  templateUrl: './supplierreviews.component.html',
  styleUrls: ['./supplierreviews.component.css']
})
export class SupplierreviewsComponent implements OnInit {
  reviewList:any
  OwnerReview:any;
  id:any;
  pageNumber:number=1
  totalItems:number=0
  pagesize:number=1
  errorMessage: any;
  reviewShowed:number=2
  viewMoreBtnShow:boolean=true
  constructor(private suppler:SupplierService,private activatedroute:ActivatedRoute)
  {

  } 
  showReview(){
    this.reviewShowed+=1

    this.reviewList=this.OwnerReview.slice(0,this.reviewShowed)
    if(this.reviewShowed==this.OwnerReview.length){
      this.viewMoreBtnShow=false
    }
  }

  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });
    this.suppler.GetAllReviews(this.id).subscribe({
      next:(data: { data: any; })=>
      {
        console.log(data);
        this.OwnerReview=data.data
        this.reviewList=this.OwnerReview.slice(0,this.reviewShowed)
          if(this.OwnerReview.length>this.reviewShowed){
            this.viewMoreBtnShow=true
          }
          else{
            this.viewMoreBtnShow=false
          }
      },
      error:(error: any)=>this.errorMessage=error
    })
  }
  // getAllreviews(pgNum: number, pageSize: number,id:number)
  // {
  //   this.suppler.GetAllReviews(this.id).subscribe({
  //     next:(data: { data: any; })=>
  //     {
  //       console.log(data);
  //       this.OwnerReview=data.data
  //       console.log("list"+this.OwnerReview);
  //     },
  //     error:(error: any)=>this.errorMessage=error
  //   })
  // }
  pageNumberChanged(value:any)
  {
    this.pageNumber = value
    // this.getAllreviews(this.pageNumber,this.pagesize,this.id)
    this.pageNumber = 1
    console.log("page"+value); 
  }
}
