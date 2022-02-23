import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any = {};
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter<boolean>();
  RegisterForm:FormGroup;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {

  }
  register(){
    // this.accountService.register(this.model).subscribe(
    //   (data) => {
    //     console.log(data);
    //     this.cancel();
    //   },
    //   error => console.log(error)
    // )
    console.log(this.RegisterForm.value);
  }
  cancel(){
    this.cancelRegister.emit(false);
  }

  initilaizeForm(){
    this.RegisterForm = new FormGroup({
      username: new FormControl("Hello", Validators.required),
      password: new FormControl("", [Validators.required, Validators.minLength(4),Validators.maxLength(8)]),
      confirmPassword: new FormControl("", Validators.required)
    });
    this.RegisterForm.get("password")?.valueChanges.subscribe(()=>{
      this.RegisterForm.get("confirmPassword")?.hasValidator;
    })
  }
matchValue(matchTo:string):ValidatorFn{
  return(control:AbstractControl):ValidationErrors |null => {
      const ControlValue = control.value;
      const ControlToMatch = (control?.parent as FormGroup)?.controls[matchTo];//.parent?.controls [matchTo];
      const ControlToMatchValue = ControlToMatch?.value;
      return (ControlValue === ControlToMatchValue) ? null : {IsMatching: true}
    
  }
}

}