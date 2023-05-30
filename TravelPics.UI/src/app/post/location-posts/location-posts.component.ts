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
  selector: 'travelpics-location-posts',
  templateUrl: './location-posts.component.html',
  styleUrls: ['./location-posts.component.scss']
})
export class LocationPostsComponent implements OnInit, OnDestroy{
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
    this.route.queryParams.subscribe((params) => {
      const locationName: string = params['locationName'];
      console.log(locationName);
      this.getLocationPosts(locationName);
    });
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  ngOnDestroy(): void {
    if (this.routeParamsSubscription) {
      this.routeParamsSubscription.unsubscribe();
    }
  }

  private getLocationPosts(locationName: string): void{
    this.isLoading = true;
    this.postService.getLocationPosts(locationName).subscribe({
      next: (data: Post[])=>{
        this.posts = data;
        this.isLoading = false;
      },
      error: (error:any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Posts',
          detail: 'Could not get location posts.',
        });
      }
    });
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }
}
