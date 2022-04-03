import { ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/models/member';
import { Message } from 'src/app/models/Message';
import { MembersService } from 'src/app/services/members.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MemberDetailComponent implements OnInit {
member!:Member;
messages:Message[] = [];
galleryOptions: NgxGalleryOptions[];
galleryImages: NgxGalleryImage[];
@ViewChild("memberTabs", {static:true}) memberTabset:TabsetComponent;
activeTab:TabDirective;

  constructor(private membersService:MembersService, private route: ActivatedRoute, private messageService:MessageService) { }

  ngOnInit(): void {
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      },
    ];

    this.galleryImages = [
      {
        small: 'assets/img/gallery/1-small.jpeg',
        medium: 'assets/img/gallery/1-medium.jpeg',
        big: 'assets/img/gallery/1-big.jpeg'
      }
    ];

    this.loadMember();
  }

  getImages() : NgxGalleryImage[]{
    const  imgUrls:NgxGalleryImage[] = [];
    for (const photo of this.member.photos) {
      imgUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url
      })
    }
          return imgUrls;
  }

loadMember(){
  const userName = this.route.snapshot.paramMap.get('username') as string;
  this.membersService.getMember(userName).subscribe(member => {
    this.member = member;
  this.galleryImages = this.getImages()});
}
onTabActivate(data:TabDirective){
this.activeTab = data;
if(this.activeTab.heading === "Messages" && this.messages.length === 0)
{}
}
loadMessages() {
  this.messageService.GetMessagesThread(this.member.userName).subscribe(m => {
    this.messages = m;
  });
}
selectTab(tadId:number){
this.memberTabset.tabs[tadId].active==true;
}

}