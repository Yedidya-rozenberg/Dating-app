import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements  OnInit {
  title = 'The dating app';
  users:any;

  constructor(private http: HttpClient, private accountService: AccountService) {

  }
  ngOnInit(): void {
// this.GetUsers();
this.setCurrentUser();

  }
  setCurrentUser() {
    const userFromLS:any = localStorage.getItem('user');
    const user = JSON.parse(userFromLS);
    this.accountService.setCurrentUser(user);
  }
//   GetUsers() {
// this.http.get('https://localhost:5001/api/users').subscribe(
//   {
// next: (Date) => {this.users = Date; console.log(Date)},
// error: (err)=>{console.log(err);}

// }
// // (res)=> {this.users = res;},
// // (err) => {console.log(err);},
// // () => {console.log('Users Loaded');}
// )
// }
}

