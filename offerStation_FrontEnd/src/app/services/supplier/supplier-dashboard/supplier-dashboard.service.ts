import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class SupplierDashboardService {

  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'SupplierAnalysis';

  GetTotalCustomers(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/Customer?supplierId='+supplierId);
    
  }
  GetTotalOrders(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/orders?supplierId='+supplierId);
    
  }
  GetTotalProfit(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/profits?supplierId='+supplierId);
    
  }
  GetTopOffers(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/offer?supplierId='+supplierId);
    
  }
  GetTopProduct(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/product?supplierId='+supplierId);
    
  }

  GetProductCount(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Products?supplierId='+supplierId);
    
  }

  
  GetOffersCount(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Offers?supplierId='+supplierId);
    
  }
  GetOrdersStatus(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/orders/status?supplierId='+supplierId);
    
  }
  GetOffersProductsCount(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/OffersOrders/Productorders?supplierId='+supplierId);
    
  }
  GetTopOrderdCustomers(supplierId:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/Customers?supplierId='+supplierId);
    
  }
}
