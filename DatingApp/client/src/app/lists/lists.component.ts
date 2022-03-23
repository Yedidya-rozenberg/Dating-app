import { Component, OnInit } from '@angular/core';
import { Member } from '../models/member';
import { Pagination } from '../models/pagination';
import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Partial<Member>[] = [];
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;
  storage = true;


  constructor(private membersService:MembersService) { }

  ngOnInit(): void {
    this.loadLikes()
  }
  loadLikes() {
    this.membersService.getLikes(this.predicate, this.pageNumber, this.pageSize, this.storage).subscribe(members => {
      this.members = members.result;
      this.pagination = members.pagination;
    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadLikes();
  }

}


