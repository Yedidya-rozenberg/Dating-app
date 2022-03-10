import { HttpClient, HttpHandler, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, take, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';
import { PaginatedResult } from '../models/pagination';
import { User } from '../models/user';
import { UserParams } from '../models/user-params';
import { AccountService } from './account.service';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
 
baseUrl = environment.apiUrl;
memberCache = new Map<string, PaginatedResult<Member[]>>();
members:Member[] = [];
  user: User;
  userParams: UserParams;

  constructor(private http:HttpClient, private accountService: AccountService) { 
    accountService.currentUser$.pipe(take(1)).subscribe((user:any)=> {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }

  getMembers(userParams:UserParams): Observable<PaginatedResult<Member[]>> {
const cacheKay = Object.values(userParams).join('-');
const response = this.memberCache.get(cacheKay);
if (response){return of (response);}

  let params = this.getPaginetionParams(userParams);
  params = params.append('minAge',userParams.minAge.toString());
  params =  params.append('maxAge',userParams.max.toString());
  params =  params.append('gender',userParams.gender.toString());
  params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<Member[]>(`${this.baseUrl}users` ,params)
    .pipe(tap(res=> this.memberCache.set(cacheKay, res)));
  }
  private getPaginatedResult<T>(url:string, params: HttpParams): Observable<PaginatedResult<T>> {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, 
      {
        observe: 'response',
        params
      }).pipe(
        map((res: HttpResponse<T>) => {
          paginatedResult.result = res.body as T;
          if (res.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(res.headers.get('Pagination') || '');
          }
          return paginatedResult;
        })
      );
  }

  getMember(userName : string): Observable<Member> {
    // const member = this.members.find(x=>x.userName === userName);
    // if (member)
    // return of (member);
    const members = [...this.memberCache.values()];
    const AllMembers = members.reduce((arr:Member[], elem: PaginatedResult<Member[]>)=> arr.concat(elem.result),[] );
    const foundMember = AllMembers.find(m=>m.userName===userName);
    if (foundMember) {return of (foundMember);}
    return this.http.get<Member>(`${this.baseUrl}users/${userName}`)
  }
  
  updateMember(member:Member){
    return this.http.put(`${this.baseUrl}users`, member).pipe(
      tap(()=>{
        const index = this.members.findIndex  (x=>x.userName===member.userName);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}users/set-main-photo/${photoId}`, {});
  }

  deletePhoto(photoId:number) {
    return this.http.delete(`${this.baseUrl}users/delete-photo/${photoId}`);
  }

  resetUserParams(): UserParams {
    this.UserParams = new UserParams(this.user)
    return this.UserParams;  
  }

  public get UserParams(): UserParams{
    return this.userParams;
  }

  public set UserParams(userParams:UserParams){
    this.userParams = userParams;
  }

private getPaginetionParams ({pageNumber, pageSize}:UserParams){
  let params = new HttpParams();
  params = params.append('pageNumber',pageNumber.toString());
  params = params.append('pageSize',pageSize.toString());
  return params;
}
}
