import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { map, Observable, switchMap } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private oidc: OidcSecurityService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return this.oidc
      .getAccessToken()
      .pipe(
        switchMap((token: string) => {
          console.log('token', token);
          const req = request.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          })

          return next.handle(req);
        })
      );
  }
}
