import { Injectable } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";

@Injectable({
  providedIn: 'root'
})
export class BuzyService {
  busyRequestCount = 0

  constructor(private spinner: NgxSpinnerService) {}

  buzy(){
this.busyRequestCount++;
this.spinner.show(
  undefined,
  {
    bdColor: 'rgb(255,255,255,0)',
    color: '#333333',
    type: 'line-scale-party'
  }
)
  }
  idle(){
    this.busyRequestCount--;
    if (this.busyRequestCount<=0)
    this.busyRequestCount = 0;
    this.spinner.hide();  }
}
