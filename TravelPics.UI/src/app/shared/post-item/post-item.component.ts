import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageService } from 'primeng/api';
import { LikeModel } from 'src/app/services/api/dtos/like-model';
import { Post } from 'src/app/services/api/dtos/post';
import { PostService } from 'src/app/services/api/post.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { ImageService } from 'src/app/services/ui/helpers/image.service';

@Component({
  selector: 'travelpics-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.scss']
})
export class PostItemComponent implements OnInit {

  @Input() post!: Post;
  @Input() loggedInUser!: UserInfo;
  @Input() responsiveOptions:any[] = [];
  @Input() getImageUrl!: (content:string, fileName:string) => string;

  constructor(    
    public imageHelperService:ImageService,
    private _postService: PostService,
    private messageService: MessageService,
    ){
  }

  public isLiked: boolean = false;
  public isHovered: boolean = false;
  public likesCount: number = 0;

  ngOnInit(): void {
    if(this.post.likes.length > 0 && this.post.likes.some(l => l.userId == this.loggedInUser.userId)){
      this.isLiked = true;
    }
  }


  public likePost(): void{
    let like: LikeModel = {
      id: 0,
      userId: this.loggedInUser.userId,
      postId: this.post.id
    }
    this._postService.likePost(like).subscribe({
      next: (data:any)=>{
        this.isLiked = true;
        this.likesCount = this.likesCount + 1;
      },
      error: (error:any)=>{
        this.messageService.add({
          severity: 'error',
          summary: 'Like Post',
          detail: 'Could not like post.',
        });
      }
    });
  }

  public dislikePost(): void{
    let like: LikeModel = {
      id: 0,
      userId: this.loggedInUser.userId,
      postId: this.post.id
    }
    this._postService.dislikePost(like).subscribe({
      next: (data:any)=>{
        this.isLiked = false;
        this.likesCount = this.likesCount - 1;
      },
      error: (error:any)=>{
        this.messageService.add({
          severity: 'error',
          summary: 'Dislike Post',
          detail: 'Could not dislike post.',
        });
      }
    });
  }
}
