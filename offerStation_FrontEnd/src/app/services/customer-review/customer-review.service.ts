import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { Review } from 'src/app/sharedClassesAndTypes/Review';

@Injectable({
  providedIn: 'root'
})
export class CustomerReviewService {
  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'Customer';
  urlowner = Base.apiUrl + 'OwnerReview';
  postReview(data:Review,customerId:number,ownerId:number):Observable<ApiResponce>{
    return this._httpClient.post<ApiResponce>(this.url + '/CustomerReview/id?CustomerId='+customerId+'&ownerId='+ownerId, data);

  }
  postOwnertReview(data:Review,ownerId:number,supplierId:number):Observable<ApiResponce>{
    return this._httpClient.post<ApiResponce>(this.urlowner + '/OwnerReview/id?ownerId='+ownerId+'&supplierId='+supplierId, data);

  }
}
