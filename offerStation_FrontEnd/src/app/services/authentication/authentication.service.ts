import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { Owner } from 'src/app/sharedClassesAndTypes/Owner';
import { Supplier } from 'src/app/sharedClassesAndTypes/Supplier';
import { User } from 'src/app/sharedClassesAndTypes/User';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {


  constructor(private _httpClient: HttpClient, private router: Router) { }

  url = Base.apiUrl + 'Account';

  userData = new BehaviorSubject(null);

  login(formData: object): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(this.url + `/login`, formData);
  }

  registerUser(user: User): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(this.url + `/Customer/register`, user);
  }
  registerOwner(owner: Owner): Observable<ApiResponce> {
    return this._httpClient.post<ApiResponce>(this.url + `/Owner/register`, owner);
  }
  registerSupplier(supplier: Supplier): Observable<ApiResponce> {
    console.log("send");
    return this._httpClient.post<ApiResponce>(this.url + `/Supplier/register`, supplier);
    
  }

  saveUserData() {    
    let encodedUserData = JSON.stringify(localStorage.getItem('userToken'))
    this.userData.next(jwtDecode(encodedUserData))
    
    // this.router.navigate(['login']);
  }

  logout() {
    localStorage.removeItem('userToken');
    this.userData.next(null)
  }
  GetToken(){
    if(localStorage.getItem('userToken'))
      return `Bearer ${localStorage.getItem('userToken') as string}`;
    return '';
  }
  testToken(ProductId:any): Observable<ApiResponce> {
    // return this._httpClient.post<ApiResponce>(`https://localhost:7017/api/Cart/addProductToCart`,ProductId);
    return this._httpClient.post<ApiResponce>(`https://localhost:7017/api/Cart/addOfferToCart`,ProductId);
  }

}
