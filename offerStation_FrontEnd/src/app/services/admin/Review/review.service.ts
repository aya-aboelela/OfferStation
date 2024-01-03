import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiURLOwner = Base.apiUrl + 'OwnerReview';
  private apiURLCustomer = Base.apiUrl + 'Customer';

  constructor(private _http: HttpClient) { }

  GetAllCustomersReviews(): Observable<any> {
    return this._http.get<any>(this.apiURLCustomer + "/AllCustomersReviews").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  DeleteCustomerReview(id:number) {
    return this._http.delete(`${this.apiURLCustomer}/CustomerReview/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }

  GetAllOwnerReviews(): Observable<any> {
    return this._http.get<any>(this.apiURLOwner + "/AllOwnersReviews").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  DeleteOwnerReview(id:number) {
    return this._http.delete(`${this.apiURLOwner}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }
}
