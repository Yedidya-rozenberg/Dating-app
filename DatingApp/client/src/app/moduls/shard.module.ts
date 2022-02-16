import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';





@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TabsModule.forRoot(),
    NgxGalleryModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule
  ],
  exports : [
    TabsModule,
    NgxGalleryModule,
    BsDropdownModule,
    ToastrModule,
    NgxSpinnerModule
  ]
})
export class ShardModule { }
