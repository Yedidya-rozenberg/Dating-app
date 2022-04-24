import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../models/Message';
import { User } from '../models/user';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection:HubConnection;


  private messagesThreadSource$ = new BehaviorSubject<Message[]>([]);
    messagesThread$ = this.messagesThreadSource$.asObservable();

  constructor(
    private http: HttpClient
  ) { }


  connectHubConnection(user:User, otherUser: User){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(`${this.hubUrl}presence?["username"] = ${user.username}`, {
      accessTokenFactory: () => user.token
    })
    .withAutomaticReconnect()
    .build();

    this.hubConnection
    .start()
    .catch(err=>console.log(err));

    this.hubConnection.on("NewMessages", message:Message)

    this.hubConnection.on()
  }


  getMessages(pageNumber:number, pageSize:number, container: string) {
    let params = getPaginationParams(pageNumber, pageSize);
    params = params.append("container", container);
    return getPaginatedResult<Message[]>(`${this.baseUrl}messages`, params, this.http);
  }

  getMessageThread(username: string) {
    return this.http.get<Message[]>(`${this.baseUrl}messages/thread/${username}`);
  }

  sendMessage(username: string, content: string){
    const createMessage = {recipientUsername:username, content};
    return this.http.post(this.baseUrl + 'messages', createMessage);
  }

  deleteMessage(id:number): Observable<any>{
    return this.http.delete(`${this.baseUrl}messages/${id}`);
  }
}
