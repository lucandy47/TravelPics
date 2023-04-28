import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Post } from 'src/app/services/api/dtos/post';
import { UserInfo } from 'src/app/services/ui/auth/user-info';

@Component({
  selector: 'travelpics-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.scss']
})
export class PostItemComponent {

  @Input() post!: Post;
  @Input() loggedInUser!: UserInfo;
  @Input() responsiveOptions:any[] = [];
  @Input() getImageUrl!: (content:string, fileName:string) => string;

  constructor(    
    private sanitizer: DomSanitizer
    ){
  }
}
