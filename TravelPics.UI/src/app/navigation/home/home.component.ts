import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageService } from 'primeng/api';
import { Post } from 'src/app/services/api/dtos/post';
import { PostService } from 'src/app/services/api/post.service';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { DocumentHelper } from 'src/app/shared/helpers/documentHelper';
import { GaleriaResponsiveOptions } from 'src/app/shared/utils/galeria-options';

@Component({
  selector: 'travelpics-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{

  constructor(
    private authUserService: AuthUserService,
    private postService: PostService,
    private messageService: MessageService,
    private sanitizer: DomSanitizer
  ){}

  public isLoading: boolean = true;
  public posts: Post[] = [];
  public loggedInUser!: UserInfo;

  public responsiveOptions: any[] = [];
  
  ngOnInit(): void {

    this.loggedInUser = this.authUserService.loggedInUser;
    this.getLatestPosts();
    
    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

  private getLatestPosts(): void{
    this.isLoading = true;
    this.postService.getLatestPosts().subscribe({
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

  public getSanitizedBlobUrlFromBase64(base64: string, fileName: string): any {
    let contentType: string = `image/${DocumentHelper.getDocumentExtension(fileName)}`;
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: contentType });
    const blobUrl = URL.createObjectURL(blob);
    const sanitizedUrl = this.sanitizer.bypassSecurityTrustUrl(blobUrl);
    return sanitizedUrl;
  }
}
