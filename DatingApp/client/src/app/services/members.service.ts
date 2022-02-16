import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl = environment.apiUrl;
members:Member[] = [];

  constructor(private http:HttpClient) { }

  getMembers(): Observable<Member[]>{
    if(this.members.length)
    return of(this.members)
    return this.http.get<Member[]>(`${this.baseUrl}users`).pipe(
      tap(members=> this.members = members)
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
}
