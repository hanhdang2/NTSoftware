import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpRequest,
  HttpResponse,
  HttpParams,
  HttpErrorResponse
} from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { BASE_API } from '../../constants/api/index';
import { CURRENT_USER } from '../../constants/localStorageKey/index';
import { GenericResult } from '../../shared/model/respose';
import { AppUser } from '../../shared/model/appUser';
import { TypeofExpr, Type } from '@angular/compiler';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  myAppUrl = '';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    body: {},
    params: new HttpParams({})
  };
  constructor(private http: HttpClient) {
    this.myAppUrl = BASE_API;
  }

  onLogin(url: string, body?: any, param?: HttpParams) {
    const user = localStorage.getItem(CURRENT_USER);
    if (user) {
      this.httpOptions.headers.set('Authorization', JSON.parse(user));
    }
    if (body) {
      this.httpOptions.body = body;
    }
    if (param) {
      this.httpOptions.params = param;
    }
    return this.http.post(`${this.myAppUrl}${url}`, body, this.httpOptions);
  }
  onRequestPassword(url: string, param?: HttpParams) {
    const user = localStorage.getItem(CURRENT_USER);
    if (user) {
      this.httpOptions.headers.set('Authorization', JSON.parse(user));
    }
    if (param) {
      this.httpOptions.params = param;
    }
    return this.http.get(`${this.myAppUrl}${url}`, this.httpOptions);
  }
  onResetPassword(url: string, body?: any, param?: HttpParams) {
    const user = localStorage.getItem(CURRENT_USER);
    if (user) {
      this.httpOptions.headers.set('Authorization', JSON.parse(user));
    }
    if (body) {
      this.httpOptions.body = body;
    }
    if (param) {
      this.httpOptions.params = param;
    }
    return this.http.put(`${this.myAppUrl}${url}`, body, this.httpOptions);
  }
  private handleError(error: HttpErrorResponse) {
    return throwError('Something bad happened; please try again later.');
  }
}
