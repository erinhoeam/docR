import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
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
        } else if (error.status === 400) {
            console.log(error.error.erros);
            errMsg = `${error.status} - ${error.error}`;
        }
        
        return throwError(errMsg);
    }

    protected obterAuthHeader(): HttpHeaders {
       
        let httpOptions = new HttpHeaders();

        httpOptions.append('Content-Type', 'application/json')

        return httpOptions;
    }
}