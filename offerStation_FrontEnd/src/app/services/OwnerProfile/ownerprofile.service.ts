import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { OwnerInfo } from 'src/app/sharedClassesAndTypes/OwnerInfo';

@Injectable({
  providedIn: 'root'
})
export class OwnerprofileService {

  private apiURL = Base.apiUrl + 'Owner';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  GetOwnerInfo(id: number): Observable<OwnerInfo> {
    return this.http.get<OwnerInfo>(`${this.apiURL}/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  UpdateOwnerInfo(id: number, profileForm: any) {
    return this.http.put<ApiResponce>(`${this.apiURL}/id?id=${id}`, profileForm).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
}
