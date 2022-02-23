import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { CoreModule } from './moduls/core.module';
import { TestErrorComponent } from './Errors/test-error/test-error.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { ShardModule } from './moduls/shard.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoudingInterceptor } from './interceptors/louding.interceptor';
import { PhotoEditorComponent } from './member-edit/photo-editor/photo-editor.component';
import { TextInputComponent } from './forms/text-input/text-input.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent
  ],
  imports: [
    ShardModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    CoreModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoudingInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
