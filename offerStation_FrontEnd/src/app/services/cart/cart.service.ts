import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiURL = Base.apiUrl + 'Cart';
  constructor(private http: HttpClient) { }
  AddProductToCart(newProduct: any): Observable<any> {
    return this.http.post<any>(this.apiURL + "/addProductToCart", newProduct).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  AddOfferToCart(newOffer: any): Observable<any> {
    return this.http.post<any>(this.apiURL + "/addOfferToCart", newOffer).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  GetCartdetails(): Observable<any> {
    return this.http.get<any>(this.apiURL + "/GetCartDetails").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");

    }));
  }
  RemoveProductToCart(productId: any): Observable<any> {
    return this.http.get<any>(this.apiURL + "/removeProductToCart?ProductId=" + productId).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  RemoveOfferToCart(productId: any): Observable<any> {
    return this.http.get<any>(this.apiURL + "/removeOfferToCart?OfferId=" + productId).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  GetCreateOrder() {
    return this.http.get<any>(this.apiURL + "/getCreateOrder").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");

    }));
  }
  MakeOrder() {
    return this.http.post<any>(this.apiURL + "/postCreateOrder",null).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");

    }));
  }
  // CreateOrder(){
  //   return this.http.get<any>(this.apiURL+"").pipe(catchError((err) => {
  //     return throwError(() => err.message || "server error");

  //   }));
  // }

  ////////////////////Owner/////////
  AddProductToOwnerCart(newProduct: any): Observable<any> {
    return this.http.post<any>(this.apiURL + "addOwnerProductToCart", newProduct).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  AddOfferToOwnerCart(newOffer: any): Observable<any> {
    return this.http.post<any>(this.apiURL + "/addOwnerOfferToCart", newOffer).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  GetOwnerCartdetails(): Observable<any> {
    return this.http.get<any>(this.apiURL + "/GetOwnerCartDetails").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");

    }));
  }
  RemoveProductToOwnerCart(productId: any): Observable<any> {
    return this.http.get<any>(this.apiURL + "/removeOwnerProductToCart?ProductId=" + productId).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  RemoveOfferToOwnerCart(productId: any): Observable<any> {
    return this.http.get<any>(this.apiURL + "/removeOwnerOfferToCart?OfferId=" + productId).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  GetCreateOwnerOrder() {
    return this.http.get<any>(this.apiURL + "/getCreateOrder").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");

    }));
  }
}
