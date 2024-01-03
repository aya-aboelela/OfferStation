import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Base } from 'src/app/sharedClassesAndTypes/Base';
import { Category } from 'src/app/sharedClassesAndTypes/Category';
import { PagedResponse } from 'src/app/sharedClassesAndTypes/PagedResponse';

@Injectable({
  providedIn: 'root'
})
export class AdminSupplierCategoryService {

  constructor(private _http: HttpClient) { }

  private url = Base.apiUrl + 'Supplier';

  GetAllCategories(pageNumber: number, pageSize: number): Observable<PagedResponse<Category>> {
    const params = { PageNumber: pageNumber.toString(), PageSize: pageSize.toString() };
    return this._http.get<PagedResponse<Category>>(this.url + "/CategoriesByPage", { params }).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  AddCategory(category: any) {
    return this._http.post(`${this.url}/SupplierCategory`, category).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  UpdateCategory(id: number, category: any) {
    return this._http.put(`${this.url}/SupplierCategory/id?id=${id}`, category).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }

  DeleteCategory(id: number) {
    return this._http.delete(`${this.url}/SupplierCategory/id?id=${id}`).pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    }));
  }
}
