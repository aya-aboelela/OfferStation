import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Observable, catchError, throwError } from 'rxjs';
import { OwnerDetails } from 'src/app/sharedClassesAndTypes/OwnerDetails';

@Injectable({
  providedIn: 'root'
})
export class OwnerService {

  url = Base.apiUrl + 'Owner';
  _ownerUrl = Base.apiUrl;
  constructor(private _httpClient: HttpClient) { }

  //Owner Product Crud Operations
  GetProductDetails(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this.url}/Product/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  AddProduct(OwnerId: number, newProduct: any): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(`${this.url}/Product/id?ownerId=${OwnerId}`, newProduct).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  DeleteProduct(id: number): Observable<ApiResponce> {
    return this._httpClient.delete<ApiResponce>(`${this.url}/Product/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  UpdateProduct(id: number, updatedProduct: any): Observable<ApiResponce> {
    return this._httpClient.put<ApiResponce>(`${this.url}/Product/id?id=${id}`, updatedProduct).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  //Owner Category Crud Operations
  GetCategoryDetails(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this._ownerUrl}OwnerMenuCategory/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  AddCategory(OwnerId: number, newCategory: any): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(`${this._ownerUrl}OwnerMenuCategory/id?ownerId=${OwnerId}`, newCategory).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  DeleteCategory(id: number): Observable<ApiResponce> {
    return this._httpClient.delete<ApiResponce>(`${this._ownerUrl}OwnerMenuCategory/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  UpdateCategory(id: number, updatedCategory: any): Observable<ApiResponce> {
    return this._httpClient.put<ApiResponce>(`${this._ownerUrl}OwnerMenuCategory/id?id=${id}`, updatedCategory).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  GetOwnerOffer(pageNumber: number, pagesize: number, ownerCategory: string, cityId: number, SortBy: string): Observable<ApiResponce> {
    console.log(ownerCategory)
    return this._httpClient.get<ApiResponce>(this.url + "/All/offers/filter/withPagination?PageNumber=" + pageNumber + "&pageSize=" + pagesize + "&category=" + ownerCategory + "&cityId=" + cityId + "&SortBy=" + SortBy);
  }

  GetOwners(pageNumber: number, pagesize: number, ownerCategory: string, cityId: number, SortBy: string, Name: string): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/All/Filter/Pagination?PageNumber=" + pageNumber + "&pageSize=" + pagesize + "&category=" + ownerCategory + "&cityId=" + cityId + "&SortBy=" + SortBy + "&name=" + Name);
  }

  getMenuCategorybyOwnerId(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/AllMenuCategoriesByOwnerId/id?id=" + id).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  getProductsByCategoryId(id: number): Observable<any> {
    return this._httpClient.get<any>(this.url + "/AllProductsByMenuCategoryID/id?id=" + id);
  }

  getAllProductsByOwnerIdWithPagination(pgNum: number, pageSize: number, id: number): Observable<any> {

    return this._httpClient.get<any>(this.url + "/GetAllProductsByOwmerIDWithPagination/id?pageNumber=" + pgNum + "&pageSize=" + pageSize + "&ownerid=" + id);
  }

  getAllProductsByOwnerId(id: number): Observable<any> {

    return this._httpClient.get<any>(this.url + "/AllProductsByOwnerID/id?ownerid=" + id);
  }

  GetAllCustomerReviewsByOwnerIdWithPagination(pgNum: number, pageSize: number, id: number): Observable<any> {
    return this._httpClient.get<any>(this._ownerUrl+ "OwnerReview/GetAllCustomerReviewsByOwnerIdWithPagination/id?pageNumber="+pgNum+"&pageSize="+pageSize+"&id="+id)
  }

  GetOwnerInfo(id: number): Observable<OwnerDetails> {
    return this._httpClient.get<OwnerDetails>(this.url + "/GetOwnerInfo?id=" + id)
  }

  GetAllOffersByOwnerId( id: number): Observable<any> {
    return this._httpClient.get<any>(this.url + "/GetAllOffersByOwnerId?id="+id).
    pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error")
    }));
  }

  //Owner Offer Crud Operations

  GetOffersByOwnerId(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(`${this._ownerUrl}OwnerOffer/all/id?ownerId=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  AddOffer(OwnerId: number, newOffer: any): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(`${this._ownerUrl}OwnerOffer/Offer/id?ownerId=${OwnerId}`, newOffer).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  DeleteOffer(id: number): Observable<ApiResponce> {
    return this._httpClient.delete<ApiResponce>(`${this._ownerUrl}OwnerOffer/Offer/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  //GetAddressByOwnerId
  GetAddressByOwnerId(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/GetAddressByOwnerId/id?id=" + id).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  //GetMinProductPriceByOwnerId
  GetMinPriceoFProductByOwmerID(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/GetMinPriceoFProductByOwmerID/id?id=" + id).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  //GetMaxProductPriceByOwnerId
  GetMaxPriceoFProductByOwmerID(id: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/GetMaxPriceoFProductByOwmerID/id?id=" + id).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  //GetProductsByOwmerIDAndPrice
  GetProductsByOwmerIDAndPrice(id: number, selectedprice: number): Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>(this.url + "/GetProductsByOwmerIDAndPrice/ownerid/selectedprice?ownerid=" + id + "&selectedprice=" + selectedprice).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  GetOfferProduct(offerId: number) {
    return this._httpClient.get<ApiResponce>(this.url + "/Offer/Products/Details?Offerid=" + offerId).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }
  //GetOfferDetails
  GetOfferDetatils(offerID:number)
  {
    return this._httpClient.get<ApiResponce>(this.url+"/GetOfferDetailsByOfferId/id?id="+offerID).
    pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error")
    }));

  }
  GetOfferdetails(offerId:number){
    return this._httpClient.get<ApiResponce>(Base.apiUrl+"OwnerOffer/id?id="+offerId).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }
  GetAllCustomerReviewByOwnerId(id:number)
  {
    return this._httpClient.get<ApiResponce>(this.url+"/AllCustomerReviewsByOwnerId/id?ownerId="+id).
    pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error")
    }));
    
  }
}