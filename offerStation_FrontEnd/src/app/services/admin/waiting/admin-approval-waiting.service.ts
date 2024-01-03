import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class AdminApprovalWaitingService {

  private apiURLOwner = Base.apiUrl + 'Owner';
  private apiURLSup = Base.apiUrl + 'Supplier';

  constructor(private _http: HttpClient) { }

  GetAllWaitingOwners(): Observable<any> {
    return this._http.get<any>(this.apiURLOwner + "/GetWaitingOwners").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  ApproveOwner(id:number) {
    return this._http.get(`${this.apiURLOwner}/Approve/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }
  DeleteOwner(id:number) {
    return this._http.delete(`${this.apiURLOwner}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }

  GetAllWaitingSuppliers(): Observable<any> {
    return this._http.get<any>(this.apiURLSup + "/GetWaitingSuppliers").pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
  ApproveSupplier(id:number) {
    return this._http.get(`${this.apiURLSup}/Approve/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }
  DeleteSupplier(id:number) {
    return this._http.delete(`${this.apiURLSup}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error"); 
    }));
  }
}
