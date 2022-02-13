import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './guards/auth.guard';
import { TestErrorComponent } from './Errors/test-error/test-error.component';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { PreventUnsavedChangeGuard } from './guards/prevent-unsaved-change.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: '',
    canActivate:[AuthGuard],
    runGuardsAndResolvers:'always',
    children:[
      { path: 'members',  loadChildren: () => import('./moduls/members.module').then(m => m.MembersModule) },
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangeGuard]},
      { path: 'lists',component: ListsComponent},
      { path: 'messages', component: MessagesComponent}
    ]
  },
  {
    path: 'errors', component: TestErrorComponent
  },
  { path:'not-found', component:NotFoundComponent},
  { path:'server-error', component:ServerErrorComponent},
  {
    path: '**',
    pathMatch: 'full',
    component: HomeComponent
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
