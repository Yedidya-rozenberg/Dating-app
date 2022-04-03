import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { Routes, RouterModule } from '@angular/router';
import { ShardModule } from './shard.module';
import { MemberMessagesComponent } from '../members/member-messages/member-messages.component';
import { MemberDetailsResolver } from '../Resolvers/member-details.resolver';

export const routes: Routes = [ 
  { path: '', component: MemberListComponent, pathMatch: 'full' },
  { path: ':username', component: MemberDetailComponent, resolve: MemberDetailsResolver }
]



@NgModule({
  declarations: [
    MemberListComponent,
    MemberDetailComponent,
    MemberMessagesComponent
  ],
  imports: [
    ShardModule,
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports :[
    RouterModule,
    MemberListComponent,
    MemberDetailComponent
  ]

})
export class MembersModule { }
