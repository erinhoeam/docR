import { HttpHeaders, HttpResponse, HttpErrorResponse, HttpEvent } from '@angular/common/http';
import { throwError } from 'rxjs';

import { environment } from './../../environments/environment';

export abstract class ServiceBase {
    
    public Token: string = "";

    constructor(){
        this.Token = localStorage.getItem('dv.service.token');
    }

    protected UrlServiceV1: string = environment.urlServiceV1;
    protected UrlViaCep: string = environment.urlServiceViaCep;

    protected serviceError(error: HttpErrorResponse | any) {
        let errMsg: string;
        if (error.error instanceof ErrorEvent) {
            errMsg = error.error.message;
        } else {
            errMsg = `${error.status} - ${error.error}`;
        }
        return throwError(errMsg);
    }

    protected extractData(response: HttpResponse<any>) {
        console.log(response.body)
        return response || {};
    }
    protected obterAuthHeader(): HttpHeaders {
        //this.Token = localStorage.getItem('dv.service.token');
        // const httpOptions = {
        //     headers: new HttpHeaders({
        //       'Content-Type':  'application/json'
        //     })
        // };
        //headers.append('Authorization', `Bearer ${this.Token}`);
        //let options = new HttpHeaders({ headers: headers });
        
        let httpOptions = new HttpHeaders();

        httpOptions.append('Content-Type', 'application/json')

        return httpOptions;
    }
}