import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from '../models/member';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  member!:Member;
  user!:User;
  constructor(private accountService: AccountService, private membersService:MembersService) { }

  ngOnInit(): void {
  }
  loadMember(){
    this.accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {if(user) this.user = user});
    this.membersService.getMember(this.user.username).subscribe(member => this.member = member);
  }
}
