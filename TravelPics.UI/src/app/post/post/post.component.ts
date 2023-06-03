import { Component, OnInit } from '@angular/core';
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
  selector: 'travelpics-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit{

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

  ngOnInit(): void {
    this.loggedInUser = this.authUserService.loggedInUser;
    this.routeParamsSubscription = this.route.params.subscribe((params) => {
      const postId: number = params['id'];
      this.getPostById(postId);
    });
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  ngOnDestroy(): void {
    if (this.routeParamsSubscription) {
      this.routeParamsSubscription.unsubscribe();
    }
  }

  private getPostById(postId: number): void{
    this.isLoading = true;
    this.postService.getPostById(postId).subscribe({
      next: (post: Post)=>{
        this.posts = [post];
        this.isLoading = false;
      },
      error: (error:any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Post',
          detail: `Could not get post with id ${postId}.`,
        });
      }
    });
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }
}
