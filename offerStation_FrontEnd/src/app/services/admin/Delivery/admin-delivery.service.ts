import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class AdminDeliveryService {

  constructor(private _http: HttpClient) { }

  private apiURL = Base.apiUrl + 'Delivery';

  AddDelivery(delivery:any) {
    return this._http.post(this.apiURL, delivery).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }

  UpdateDelivery(id:number, delivery:any) {
    return this._http.put(`${this.apiURL}/id?id=${id}`, delivery).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }

  DeleteDelivery(id:number) {
    return this._http.delete(`${this.apiURL}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }
}
