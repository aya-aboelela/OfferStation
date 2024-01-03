import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerReviewService } from 'src/app/services/customer-review/customer-review.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { Review } from 'src/app/sharedClassesAndTypes/Review';
import { orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Component({
  selector: 'app-owner-review',
  templateUrl: './owner-review.component.html',
  styleUrls: ['./owner-review.component.css']
})
export class OwnerReviewComponent {
  review:Review={rating:0,comment:""};
  NewReviewForm:FormGroup;
  @Input() ownerId!:number;
  @Input() supplierId!:number;
  @Input() OrderId!:number;
  @Output() modelClose: EventEmitter<number> = new EventEmitter<number>()



  constructor(private CustomerReviewService:CustomerReviewService,private router:Router ,private route:ActivatedRoute,
  private fb:FormBuilder,private orderService:OrdersService) 
  {
    this.NewReviewForm = this.fb.group({

      Rating:['',[Validators.required,Validators.min(1),Validators.max(5)]],
      comment:['',[Validators.required,Validators.minLength(4),Validators.maxLength(200)]],

    })
    this.NewReviewForm.get('Rating')?.valueChanges.subscribe((data) => {
      this.review.rating = data;
    });   
    this.NewReviewForm.get('comment')?.valueChanges.subscribe((data) => {
      this.review.comment= data;
    });
  }
  
  get Rating()
  {
    return this.NewReviewForm.get('Rating');
  }
  
  get comment()
  {
    return this.NewReviewForm.get('comment');
  }
  
  ngOnInit(): void {
    
  }

  SaveReview(){
      this.CustomerReviewService.postOwnertReview(this.review,this.ownerId,this.supplierId).subscribe((res) => {
        if (res.success) {
           console.log("success");
           console.log()
           this.orderService.OwnerOrderStatus(this.OrderId,orderStatus.delivered).subscribe((res) => {
            if (res.success) {   
              this.modelClose.emit(1)
            } else {
              console.log(res.message); 
            }
          })
        } 
        else {
          console.log(res.message); 
        }
      })
     
  }

}
