import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { BuzyService } from '../services/buzy.service';

@Injectable()
export class LoudingInterceptor implements HttpInterceptor {

  constructor(private buzyService:BuzyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
  this.buzyService.buzy();
  
    return next.handle(request).pipe(
        delay(1000),
        finalize( ()=>   this.buzyService.idle())

    )
  }
}
