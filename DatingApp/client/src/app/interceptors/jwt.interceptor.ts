import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { Gender } from '../models/gender.enum';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private account: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User = {token:'', username:'', photoUrl:'',knownUs:'', gender: Gender.female};
    this.account.currentUser$.pipe(take(1)).subscribe(user => {if (user) currentUser = user});
    if (currentUser.token){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }
    return next.handle(request);
  }
}
