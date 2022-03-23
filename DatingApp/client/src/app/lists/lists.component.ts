import { Component, OnInit } from '@angular/core';
import { Member } from '../models/member';
import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Partial<Member>[] = [];
  predicate = 'liked';

  constructor(private membersService:MembersService) { }

  ngOnInit(): void {
    this.loadLikes()
  }
  loadLikes() {
    this.membersService.getLikes(this.predicate).subscribe(members => {
      this.members = members;
    })
  }

}


