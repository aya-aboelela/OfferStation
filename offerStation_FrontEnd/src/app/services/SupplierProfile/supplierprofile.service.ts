import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SupplierInfo } from 'src/app/sharedClassesAndTypes/SupplierInfo';


@Injectable({
  providedIn: 'root'
})
export class SupplierprofileService {

  private apiURL = Base.apiUrl + 'Supplier';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  GetSupplierById(id: number): Observable<SupplierInfo> {
    return this.http.get<SupplierInfo>(`${this.apiURL}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  UpdateSupplierInfo(id: number, profileForm: any) {
    return this.http.put(`${this.apiURL}/id?id=${id}`, profileForm).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
}
