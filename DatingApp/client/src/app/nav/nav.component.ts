import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any ={};
loggedin: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    console.log(this.model);
  }
  login(){
    this.accountService.login(this.model)
    .subscribe({next: Response=> {
      console.log(Response);
      this.loggedin = true;},
    error: (error)=> console.log('faild to login', error),
  complete: ()=>{}});
      //.subscribe(Response => {console.log(Response);
      // this.loggedin = true;}
  }
  logout(){
    this.loggedin = false;
  }

}
