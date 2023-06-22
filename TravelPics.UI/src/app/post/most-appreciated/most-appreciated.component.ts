import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageService } from 'primeng/api';
import { Post } from 'src/app/services/api/dtos/post';
import { PostService } from 'src/app/services/api/post.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { ImageService } from 'src/app/services/ui/helpers/image.service';
import { GaleriaResponsiveOptions } from 'src/app/shared/utils/galeria-options';

@Component({
  selector: 'travelpics-most-appreciated',
  templateUrl: './most-appreciated.component.html',
  styleUrls: ['./most-appreciated.component.scss']
})
export class MostAppreciatedComponent implements OnInit{

  constructor(
    private authUserService: AuthUserService,
    private postService: PostService,
    private messageService: MessageService,
    private sanitizer: DomSanitizer,
    private imageHelperService: ImageService
  ){}

  public isLoading: boolean = true;
  public posts: Post[] = [];
  public loggedInUser!: UserInfo;

  public responsiveOptions: any[] = [];
  
  ngOnInit(): void {

    this.loggedInUser = this.authUserService.loggedInUser;
    this.getMostAppreciatedPosts();
    
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  private getMostAppreciatedPosts(): void{
    this.isLoading = true;
    this.postService.getMostAppreciatedPosts().subscribe({
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
