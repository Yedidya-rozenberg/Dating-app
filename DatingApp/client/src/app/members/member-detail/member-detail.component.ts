import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/member';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
member!:Member;
  constructor(private membersService:MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }
loadMember(){
  const userName = this.route.snapshot.paramMap.get('username') as string;
  this.membersService.getMember(userName).subscribe(member => this.member = member);
}
}
