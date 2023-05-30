import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { MapPost } from 'src/app/services/api/dtos/map-post';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { ImageService } from 'src/app/services/ui/helpers/image.service';
import { MapService } from 'src/app/services/ui/map/map.service';

@Component({
  selector: 'travelpics-map-posts',
  templateUrl: './map-posts.component.html',
  styleUrls: ['./map-posts.component.scss']
})
export class MapPostsComponent implements OnInit{

  constructor(
    private _mapService: MapService,
    private authUserService: AuthUserService,
    private messageService: MessageService,
    public imageHelperService: ImageService,
    private router: Router
    ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authUserService.loggedInUser;
    this.getMapPosts();
  }
  public isLoading: boolean = true;
  public loggedInUser!: UserInfo;

  public responsiveOptions: any[] = [];
  posts: MapPost[] = [];
  
  private getMapPosts(): void{
    this.isLoading = true;
    this._mapService.mapPosts.subscribe({
      next: (data)=>{
        this.posts = data;
        if(this.posts.length == 0){
          this.router.navigate(['navigation/map']);
        }
        this.isLoading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Posts',
          detail: 'Could not get map posts.',
        });
      }
    })
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }

}
