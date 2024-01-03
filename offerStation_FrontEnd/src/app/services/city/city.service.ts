import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponce } from 'src/app/sharedClassesAndTypes/ApiResponce';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private _httpClient: HttpClient) { }

  url = Base.apiUrl + 'Address';

  GetAllCities():Observable<ApiResponce> {
    return this._httpClient.get<ApiResponce>( this.url + `/cities`);
  }

}
