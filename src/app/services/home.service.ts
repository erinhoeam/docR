import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http'
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { ServiceBase } from './service.base';
import { Documento } from './../model/documento';

@Injectable()
export class HomeService extends ServiceBase {

  constructor(private http: HttpClient) { super() }

  upload(file:File) : Observable<Documento[]>{
    let httpOptions = this.obterAuthHeader();

    let formData:FormData = new FormData();
    formData.append('file', file, file.name);
    
    let response = this.http
    .post(`${this.UrlServiceV1}upload-image`, formData, { headers: httpOptions })
    .pipe(map((response: any) => <Documento[]>response.data), catchError(super.serviceError));

    return response;
  }

}
