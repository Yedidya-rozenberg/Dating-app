import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangeGuard implements CanDeactivate<MemberEditComponent> {
  canDeactivate(
    component: MemberEditComponent): boolean {
    return confirm("Are you sure you want to close is? any change not saved.");
    return true;
  }
  
}
