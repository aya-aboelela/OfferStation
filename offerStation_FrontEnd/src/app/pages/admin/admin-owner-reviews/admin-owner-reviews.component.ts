import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ReviewService } from 'src/app/services/admin/Review/review.service';
import { ReviewInfo } from 'src/app/sharedClassesAndTypes/ReviewInfo';

@Component({
  selector: 'app-admin-owner-reviews',
  templateUrl: './admin-owner-reviews.component.html',
  styleUrls: ['./admin-owner-reviews.component.css']
})
export class AdminOwnerReviewsComponent {

  reviews: ReviewInfo[] = [];

  dtOptions:DataTables.Settings = {};
  dtTrigger:Subject<any> = new Subject<any>(); 

  constructor(private _reviewService:ReviewService) {}

  ngOnInit(): void {

    this.dtOptions={
      pagingType:'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true,
      destroy:true
    }
    this.getReviews();
  }

  getReviews(): void {
    this._reviewService.GetAllOwnerReviews()
      .subscribe(response => 
        {
          this.reviews = response.data
          this.dtTrigger.next(null);
        });
  }
  onDelete(index:number, reviewId:number){
    this._reviewService.DeleteOwnerReview(reviewId)
    .subscribe({
      next: data => {    
        this.dtTrigger.unsubscribe();
        this.reviews.splice(index, 1);
        this.getReviews();
      }
    });
  }
}
