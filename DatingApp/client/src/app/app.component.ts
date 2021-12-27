import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements  OnInit {
  title = 'The dating app';
  users:any;

  constructor(private http:HttpClient){

  }
  ngOnInit(): void {
this.GetUsers();
  }
  GetUsers() {
this.http.get('https://localhost:5001/api/users').subscribe(
//   {
// next: (Date) => {this.users = Date;},
// error: (err)=>{console.log(err);}

// }
res=> {this.users = res;},
err => {console.log(err);},
() => {console.log('Users Loaded');}
)
}
}

