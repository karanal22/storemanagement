import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PaginationParam } from '../models/request/paggination-patam.model';
import { PaginationModel } from '../models/response/api-response.model';
import { StoreProductListModel } from '../models/response/store-product-list.model';

@Injectable({
  providedIn: 'root'
})
export class StoreProductService {

  constructor(private _http: HttpClient) { }


  getStoreProductList(option?: PaginationParam & {
    search?: string,
  }): Observable<PaginationModel<StoreProductListModel>> {
    let params: any = {};
    if (option) {
      params = { ...option };
      Object.keys(params).forEach(key => params[key] == null && delete params[key]);
    }

    return this._http.post<any>(`${environment.domain}/v1/StoreProdct/list`, option);
  }

  public UploadPriceFeed(file: File): any {
    const data = new FormData();
    data.append('file', file, file.name);
    return this._http.post<any>(`${environment.domain}/v1/StoreProdct/UploadPriceFeed`, data, {
      headers: {
        'Accept': 'multipart/form-data',
      }
    });
  }

  getStoreProductById(id: number): Observable<StoreProductListModel> {
    return this._http.get<any>(`${environment.domain}/v1/StoreProdct/` + id);
  }

  updateStoreProduct(data: any): Observable<any> {
    return this._http.put<any>(`${environment.domain}/v1/StoreProdct/`, data);
  }
}
