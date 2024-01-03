import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'SupplierAnalysis';

}
