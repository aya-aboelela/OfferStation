import { Component,NO_ERRORS_SCHEMA, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OwnerService } from 'src/app/services/owner/owner.service';

@Component({
  selector: 'app-ownerreview',
  templateUrl: './ownerreview.component.html',
  styleUrls: ['./ownerreview.component.css'],

})
export class OwnerreviewComponent implements OnInit {
  reviewList:any
  customerreview:any;
  pageNumber=1;
  pagesize=3;
  totalItems=0;
  errorMessage: any;
  id:any
  reviewShowed:number=2
  viewMoreBtnShow:boolean=true
  constructor(private owner:OwnerService,private activatedroute:ActivatedRoute)
  {
    
  } 
  showReview(){
    this.reviewShowed+=1

    this.reviewList=this.customerreview.slice(0,this.reviewShowed)
    if(this.reviewShowed==this.customerreview.length){
      this.viewMoreBtnShow=false
    }
  }
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap=>
      {
         this.id=Number(paramMap.get('id'));
      
      });
      
      this.owner.GetAllCustomerReviewByOwnerId(this.id).subscribe({
        next:data=>
        {
          console.log(data);
          this.customerreview=data.data
          console.log("list"+this.customerreview);
          this.reviewList=this.customerreview.slice(0,this.reviewShowed)
          if(this.customerreview.length>this.reviewShowed){
            this.viewMoreBtnShow=true
          }
          else{
            this.viewMoreBtnShow=false
          }
        },
        error:error=>this.errorMessage=error
      })
  
  }
//   getAllReviews(pgNum: number, pageSize: number,id:number) 
//   {
// this.owner.GetAllCustomerReviewsByOwnerIdWithPagination(this.pageNumber,this.pagesize,this.id).subscribe({
//   next:data=>
//   {
//     console.log(data);
//     this.customerreview=data.data
//     console.log("list"+this.customerreview);
//   },
//   error:error=>this.errorMessage=error
// })
// }

  
}
