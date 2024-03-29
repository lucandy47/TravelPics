import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { Post } from 'src/app/services/api/dtos/post';
import { PostService } from 'src/app/services/api/post.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { ImageService } from 'src/app/services/ui/helpers/image.service';
import { GaleriaResponsiveOptions } from 'src/app/shared/utils/galeria-options';

@Component({
  selector: 'travelpics-user-posts',
  templateUrl: './user-posts.component.html',
  styleUrls: ['./user-posts.component.scss']
})
export class UserPostsComponent implements OnInit, OnDestroy{

  constructor(
    private authUserService: AuthUserService,
    private postService: PostService,
    private messageService: MessageService,
    public imageHelperService: ImageService,
    public route: ActivatedRoute
  ){}

  private routeParamsSubscription: Subscription | undefined;

  public isLoading: boolean = true;
  public posts: Post[] = [];
  public loggedInUser!: UserInfo;

  public responsiveOptions: any[] = [];
  public message: string = "No posts found for specified user.";

  ngOnInit(): void {
    this.loggedInUser = this.authUserService.loggedInUser;
    this.routeParamsSubscription = this.route.params.subscribe((params) => {
      const userId: number = params['id'];
      this.getUserPosts(userId);
    });
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  ngOnDestroy(): void {
    if (this.routeParamsSubscription) {
      this.routeParamsSubscription.unsubscribe();
    }
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
          detail: 'Could not get user posts.',
        });
      }
    });
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }
}
