import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any ={};
loggedIn: boolean = false;
//correntUser$ : Observable<User | null>;

  constructor(private accountService: AccountService) {
    // this.correntUser$ = this.accountService.correntUser$;
   }

  ngOnInit(): void {
    //console.log(this.model);
    this.getCurrnetUser();
  }
  login(){
    this.accountService.login(this.model)
    .subscribe({next: Response=> {
      console.log(Response);
      this.loggedIn = true;},
    error: (error)=> console.log('faild to login', error),
  complete: ()=>{console.log('Login complete')}});
      //.subscribe(Response => {console.log(Response);
      // this.loggedin = true;}
  }
  getCurrnetUser() {
    this.accountService.currentUser$.subscribe((user:User | null) => {
      this.loggedIn = !!user;
    });
  }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
  }

}
