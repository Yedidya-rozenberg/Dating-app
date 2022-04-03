import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../models/Message';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
baseUrl = environment.apiUrl;
  constructor(
    private http:HttpClient
  ) { }
  GetMessages(pageNumber: number, pageSize: number, container:string)
  {
    let params = getPaginationParams(pageNumber,pageSize);
    params = params.append("container", container);
    return getPaginatedResult<Message[]>(`${this.baseUrl}messages`, params, this.http);
  }
  GetMessagesThread(username:string){
    return this.http.get<Message[]>(this.baseUrl+`messages/thread/${username}`)
  }
  sandMessage(username:string, content:string){
    const creatMessage = {recipientUsername:username, content};
    return this.http.post(this.baseUrl+"messages", creatMessage);
  }
}
