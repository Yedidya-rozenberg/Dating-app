import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/models/Message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
@Input() username:string;
messages:Message[];

  constructor(private messageService:MessageService) { }

  ngOnInit(): void {
    this.loadMessages()
  }
  loadMessages() {
    this.messageService.GetMessagesThread(this.username).subscribe(m => {
      this.messages = m;
    });
  }
}
