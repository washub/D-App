import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { expressionType } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(err => {
        if(err.status === 401)
        return throwError(err.statusText);
        const serverError = err.error.errors;
        let errorString = '';
        if (err && typeof err === 'object') {
          // tslint:disable-next-line: forin
          for (const key in serverError) {
            errorString += serverError[key] + '\n';
          }
        }
        return throwError(errorString || err.error || 'Unknown Error');

      })
    );
}
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
