import { Injectable, isDevMode } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable()
export class BaseUrlHttpInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!request.url.startsWith('/api')) {
            return next.handle(request);
        }

        const updatedRequest = request.clone({
            url: environment.apiUrl + request.url
        });

        return next.handle(updatedRequest);
    }
}
