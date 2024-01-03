import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { orderStatus } from 'src/app/sharedClassesAndTypes/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  url = Base.apiUrl + 'Order';
  _ownerUrl = Base.apiUrl;
  constructor(private _httpClient: HttpClient) { }

  //Owner Product Crud Operations
  GetCustomerOrders(Customerid: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/customerOrders?customerId=${Customerid}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  GetOwnerOrders(Ownerid: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/ownerOrders?ownerId=${Ownerid}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  CustomerOrderStatus(orderId: number,status:number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/CustomerOrderStatus?id=${orderId}&status=${status}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  OwnerOrderStatus(orderId: number,status:orderStatus): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/OwnerOrderStatus?id=${orderId}&status=${status}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  getOwnerOrdersRequested(ownerId: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/ownerOrdersRequested?ownerId=${ownerId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  
  getOwnerOrdersRequestedAfterShipped(ownerId: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/ownerOrdersRequestedAfterShipped?ownerId=${ownerId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  
  getSupplierOrdersRequested(supplierId: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/supplierOrdersRequested?supplierId=${supplierId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  getSupplierOrdersRequestedAfterShipped(supplierId: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/supplierOrdersRequestedAfterShipped?supplierId=${supplierId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  getPendingCustomerOrders():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/pendingCustomersOrders`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  getPendingOwnerOrders():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/pendingOwnersOrders`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  
  postCustomerOrderDelivary(customerOrderId:number,deliveryId:number,):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/customerOrderDelivery?customerOrderId=${customerOrderId}&deliveryId=${deliveryId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  postOwnerOrderDelivary(OwnerOrderId:number,deliveryId:number,):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/ownerOrderDelivery?ownerOrderId=${OwnerOrderId}&deliveryId=${deliveryId}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }
  GetAllDeliveres():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/AllDeliveries`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

}
