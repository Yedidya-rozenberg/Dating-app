import { Component, OnInit } from '@angular/core';
import { Message } from '../models/Message';
import { Pagination } from '../models/pagination';
import { UserParams } from '../models/user-params';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[] = [];
  pagination: Pagination;
  container: string = 'Inbox';
  pageNumber: number = 1;
  pageSize: number = 5;
  loadind:boolean = false;

  constructor(private messageService:MessageService) { }

  ngOnInit(): void {
    this.leadMessages()
  }
  leadMessages(
  ){
    this.loadind = true;
    this.messageService.GetMessages(this.pageNumber, this.pageSize, this.container).subscribe(
      x=> {
      this.messages = x.result;
      this.pagination = x.pagination;
    this.loadind = false;
      })
  }

  pageChanged(event:any):void{
//    if(this.pageNumber !== event.page)
//    {
      this.pageNumber = event.page;
      this.leadMessages();
   // }
  }

}
