import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { post } from 'jquery';
import { Observable, catchError, throwError } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { Category } from 'src/app/sharedClassesAndTypes/Category';
import { PagedResponse } from 'src/app/sharedClassesAndTypes/PagedResponse';

@Injectable({
  providedIn: 'root'
})
export class AdminCategoriesService {

  constructor(private _http: HttpClient) { }

  private url = Base.apiUrl + 'Owner';

  GetAllCategories(pageNumber: number, pageSize: number): Observable<PagedResponse<Category>> {
    const params = { PageNumber: pageNumber.toString(), PageSize: pageSize.toString() };
    return this._http.get<PagedResponse<Category>>(this.url + "/CategoriesByPage", { params }).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  AddCategory(category: any) {
    return this._http.post(`${this.url}/OwnerCategory`, category).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  UpdateCategory(id: number, category: any) {
    return this._http.put(`${this.url}/OwnerCategory/id?id=${id}`, category).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  DeleteCategory(id: number) {
    return this._http.delete(`${this.url}/OwnerCategory/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
}
