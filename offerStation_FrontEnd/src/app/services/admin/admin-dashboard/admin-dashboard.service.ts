import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class AdminDashboardService {

  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'AdminAnalysis';

  GetTotalCustomers():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/Customer/');
    
  }
  GetTotalOwners():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/owners/');
    
  }
  GetTotalSuppliers():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/Supplier/');
    
  }
  GetTotalOrders():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/orders/');
    
  }
  GetTotalProduct():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Products/');
    
  }
  GetTotalOffers():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Offers/');
    
  }
  GetProfits():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/profits/');
    
  }
  GetOrderedOffers():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/OffersOrders/owner/supplier');
    
  }
  GetOrderedProducts():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/ProductOrders/owner/supplier');
    
  }
  GetTotalOrderedCustomer():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/Ordered/Customers');
    
  }
  GetTotalOrderedOwners():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(  this.url + '/Total/Ordered/Owners');
    
  }
 
}
