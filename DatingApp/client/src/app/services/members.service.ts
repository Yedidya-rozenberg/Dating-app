import { HttpClient, HttpHandler, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';
import { PaginatedResult } from '../models/pagination';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl = environment.apiUrl;
members:Member[] = [];
paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http:HttpClient) { }

  getMembers(page?:number, itemsPerPage?: number): Observable<PaginatedResult<Member[]>> {
    let params = new HttpParams();
    if(page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString())
      params = params.append("pageSize", itemsPerPage.toString())
    }

    return this.http.get<Member[]>(`${this.baseUrl}users`,
    {
      observe: 'response',
      params
    }).pipe(
      map((res: HttpResponse<Member[]>) => {
        this.paginatedResult.result = res.body as Member[];
        if(res.headers.get('Pagination') !== null){
          this.paginatedResult.pagination = JSON.parse(res.headers.get('Pagination') || '');
        }
        return this.paginatedResult;
      })
    )
  }
  getMember(userName : string): Observable<Member> {
    const member = this.members.find(x=>x.userName === userName);
    if (member)
    return of (member);
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

}
