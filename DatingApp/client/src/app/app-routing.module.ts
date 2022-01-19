import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'lists',
    component: ListsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'messages',// localhost:4200/messages
    component: MessagesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '',
    canActivate:[AuthGuard],
    runGuardsAndResolvers:'always',
    children:[

      { path: 'members',
       loadChildren: () => import('./moduls/members.module').then(m => m.MembersModule)
       },
     { path: '',
       component: MemberListComponent,
       pathMatch: 'full'},
      {path: 'members/:id',// localhost:4200/members/4
    component: MemberDetailComponent}
    ]
  },
  {
    path: '**', // localhost:4200/non-existing-route/asdasd/asdasd/
    pathMatch: 'full',
    component: HomeComponent
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
