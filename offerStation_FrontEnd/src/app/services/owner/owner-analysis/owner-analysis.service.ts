import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class OwnerAnalysisService {

  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'OwnerAnalysis';

  GetTotalCustomers(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/Customer?ownerid='+ownerid);
    
  }
  GetTotalOrders(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/orders?ownerid='+ownerid);
    
  }
  GetTotalProfit(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Total/profits?ownerid='+ownerid);
    
  }
  GetTopOffers(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/offer?ownerid='+ownerid);
    
  }
  GetTopProduct(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/product?ownerid='+ownerid);
    
  }

  GetProductCount(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Products?ownerid='+ownerid);
    
  }

  
  GetOffersCount(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/Offers?ownerid='+ownerid);
    
  }
  GetOrdersStatus(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/orders/status?ownerid='+ownerid);
    
  }
  GetOffersProductsCount(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Count/OffersOrders/Productorders?ownerid='+ownerid);
    
  }
  GetTopOrderdCustomers(ownerid:any):Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + '/Top/Customers?ownerid='+ownerid);
    
  }
}
