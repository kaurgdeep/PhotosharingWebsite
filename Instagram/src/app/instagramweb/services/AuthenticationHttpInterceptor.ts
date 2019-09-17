import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { share } from 'rxjs/operators';
import { AuthenticationStore } from './AuthenticationStore';


@Injectable()
export class AuthenticationHttpInterceptor implements HttpInterceptor {
    constructor(
        private authenticationStore: AuthenticationStore, private router: Router) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let handler: Observable<HttpEvent<any>>;
        const authenticationToken = this.authenticationStore.getAuthenticationToken();

        if (authenticationToken && authenticationToken.length > 0) {
            const authenticatedRequest: HttpRequest<any> = request.clone({
                headers: request.headers.append('Authorization', 'Bearer ' + authenticationToken)
            });

            handler = next.handle(authenticatedRequest).pipe(share());

            handler.subscribe((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    const response: HttpResponse<any> = event;
                    // Handle successful http response
                    // TODO: refresh auth token

                    // alert(JSON.stringify(response.headers.keys()));
                }
            }, (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 401) {
                        // Expired auth token?
                    }
                }
            });
        } else {
            handler = next.handle(request);
        }

        return handler;
    }
}
