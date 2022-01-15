import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode = false;
Users:any;

constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
  registerToggle(){
    this.registerMode = !this.registerMode;
  }
  getUsers(){
this.http.get('https://localhost:5001/users')
.subscribe({next: (data)=> this.Users = data, error: (err)=> console.log(err)});
  }
  cancelRegisterMode($event: boolean){
    this.registerMode = $event;
  }

}
