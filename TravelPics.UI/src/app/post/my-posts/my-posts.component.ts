import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Post } from 'src/app/services/api/dtos/post';
import { PostService } from 'src/app/services/api/post.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { GaleriaResponsiveOptions } from 'src/app/shared/utils/galeria-options';
import { ImageService } from 'src/app/services/ui/helpers/image.service';

@Component({
  selector: 'travelpics-my-posts',
  templateUrl: './my-posts.component.html',
  styleUrls: ['./my-posts.component.scss']
})
export class MyPostsComponent implements OnInit{

  constructor(
    private authUserService: AuthUserService,
    private postService: PostService,
    private messageService: MessageService,
    public imageHelperService: ImageService
  ){}

  public isLoading: boolean = true;
  public posts: Post[] = [];
  public loggedInUser!: UserInfo;

  public responsiveOptions: any[] = [];

  ngOnInit(): void {
    this.loggedInUser = this.authUserService.loggedInUser;
    this.getUserPosts(this.loggedInUser.userId);
    
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  private getUserPosts(userId: number): void{
    this.isLoading = true;
    this.postService.getUserPosts(userId).subscribe({
      next: (data: Post[])=>{
        this.posts = data;
        this.isLoading = false;
      },
      error: (error:any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Posts',
          detail: 'Could not get posts.',
        });
      }
    });
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }
  
}
