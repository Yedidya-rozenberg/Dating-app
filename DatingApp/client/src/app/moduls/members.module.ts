import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [ 
  { path: '', component: MemberListComponent, pathMatch: 'full' },
  { path: ':id', component: MemberDetailComponent },
]



@NgModule({
  declarations: [
    MemberListComponent,
    MemberDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],

})
export class MembersModule { }
