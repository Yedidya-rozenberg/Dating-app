import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() userFromHomeComponent:any;
@Output() cancelRegister = new EventEmitter<boolean>();
  constructor() { }

  ngOnInit(): void {
  }
  register(){
    
  }
  cancel(){
this.cancelRegister.emit(false);
  }

}
