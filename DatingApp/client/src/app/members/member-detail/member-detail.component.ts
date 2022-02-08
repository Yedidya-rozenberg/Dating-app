import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
member!:Member;
galleryOptions: NgxGalleryOptions[];
galleryImages: NgxGalleryImage[];
  constructor(private membersService:MembersService, private route: ActivatedRoute) { }

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
}
