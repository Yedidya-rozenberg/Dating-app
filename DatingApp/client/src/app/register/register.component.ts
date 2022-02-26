import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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
  registerForm:FormGroup;
  maxDate: Date;


  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  register(){
    // this.accountService.register(this.model).subscribe(
    //   (data) => {
    //     console.log(data);
    //     this.cancel();
    //   },
    //   error => console.log(error)
    // )
    console.log(this.registerForm.value);
  }
  cancel(){
    this.cancelRegister.emit(false);
  }

  initializeForm(){

    this.registerForm = this.fb.group({
      gender:['male'],
      knownAs:["", Validators.required],
      city:["", Validators.required],
      country:["", Validators.required],
      dateOfBirth:["", Validators.required],
      username:["", Validators.required],
      password:["", [Validators.required, Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword:["",[ Validators.required, this.matchValue("password")]] 
    });
    // this.registerForm = new FormGroup({
    //   username: new FormControl("Hello", Validators.required),
    //   password: new FormControl("", [Validators.required, Validators.minLength(4),Validators.maxLength(8)]),
    //   confirmPassword: new FormControl("",[ Validators.required, this.matchValue("password")])
    // });
    this.registerForm.get("password")?.valueChanges.subscribe(()=>{
      this.registerForm.get("confirmPassword")?.updateValueAndValidity;
    })
  }
matchValue(matchTo:string):ValidatorFn{
  return(control:AbstractControl):ValidationErrors |null => {
      const ControlValue = control.value;
      const ControlToMatch = (control?.parent as FormGroup)?.controls[matchTo];
      const ControlToMatchValue = ControlToMatch?.value;
      return (ControlValue === ControlToMatchValue) ? null : {IsMatching: true}
    
  }
}

}