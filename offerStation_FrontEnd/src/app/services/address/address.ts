import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { city } from 'src/app/sharedClassesAndTypes/city';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { AddressDetails } from 'src/app/sharedClassesAndTypes/AddressDetails';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';

@Injectable({
  providedIn: 'root'
})
export class AddressServiceService {

  _url = Base.apiUrl + 'Address';
  errorMessage: any;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) { }

  GetAllCities(): Observable<city[]> {
    return this.http.get<city[]>(this._url + "/cities").pipe(
      catchError((err) => {

        return throwError(() => err.message || 'server error');
      })
    );
  }

  GetUserAdresses(ApplicationUserId: string): Observable<ApiResponce> {
    return this.http.get<ApiResponce>(this._url + "/all/id?userId=" + ApplicationUserId).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  GetAdressesDetails(id: number): Observable<ApiResponce> {
    return this.http.get<ApiResponce>(`${this._url}/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "Server Error");
      }));
  }

  AddAddress(ApplicationUserId: string, newAdress: any): Observable<ApiResponce> {
    return this.http.post<ApiResponce>(`${this._url}/id?userId=${ApplicationUserId}`, newAdress).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  DeleteAddress(id: number): Observable<ApiResponce> {
    return this.http.delete<ApiResponce>(`${this._url}/id?id=${id}`).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }

  UpdateAddress(id: number, updatedAddress: any): Observable<ApiResponce> {
    return this.http.put<ApiResponce>(`${this._url}/id?id=${id}`, updatedAddress).
      pipe(catchError((err: any) => {
        return throwError(() => err.message || "server error")
      }));
  }
}
