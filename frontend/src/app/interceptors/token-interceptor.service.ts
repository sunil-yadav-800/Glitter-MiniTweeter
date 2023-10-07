import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { ServiceService } from '../service.service';
import { catchError, isEmpty } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  // excludeCalls = [
  //   '/login',
  //   '/register'
  // ];
  token : string = "";
  constructor(private service: ServiceService, private router:Router) 
  {

  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
     this.token  = this.service.getLocalData("token");
    request = request.clone({
      setHeaders: {
        Authorization: `${this.token}`,
      },
    });
    return next.handle(request);
  }
}
