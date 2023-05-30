import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { MapPost } from 'src/app/services/api/dtos/map-post';
import { PostService } from 'src/app/services/api/post.service';
import { AlgoService } from 'src/app/services/ui/DBSCAN/algo.service';
import { PostsCluster } from 'src/app/services/ui/DBSCAN/post-cluster';
import { CustomMapMarker } from '../models/custom-map-marker';
import { Image } from "../../services/api/dtos/image";
import { ImageService } from 'src/app/services/ui/helpers/image.service';
import { Router } from '@angular/router';
import { MapService } from 'src/app/services/ui/map/map.service';

@Component({
  selector: 'travelpics-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit{

  constructor(
    private _postService: PostService,
    private messageService: MessageService,
    public algoService: AlgoService,
    public imageHelperService: ImageService,
    private router: Router,
    private _mapService: MapService
  ){}

  ngOnInit(): void {
    this.getAllPosts();
    this.zoom = 3;
  }

  public isLoading: boolean = true;
  public zoom!:number;

  markers: CustomMapMarker[] = [];
  clusters: PostsCluster[] = [];

  public mapPosts: MapPost[] = [];
  
  private getAllPosts(): void{
    this.isLoading = true;
    this._postService.getMapPosts().subscribe({
      next: (data: MapPost[]) => {
        this.mapPosts = data;
        this.isLoading = false;
        this.updateMap(this.zoom);
      },
      error: (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Posts',
          detail: 'Could not retrieve posts.',
        });
      }
    })
  }
  
  private updateMap(zoomLevel: number): void{
    this.clusters = this.algoService.getPostsClustered(zoomLevel, this.mapPosts);
    this.markers = []; 

    for(let c of this.clusters){
      let marker: CustomMapMarker = {
        lat: c.latitude,
        long: c.longitude,
        photos: this.getClusterPhotos(c),
        icon: c.clusterProfileImageSrc,
        posts: c.posts
      };
      // this.resizeImage(marker.icon, 32, 32)
      //   .then(resizedImageUrl => {
      //     marker.icon = resizedImageUrl;
      //   })
      //   .catch(error => {
      //     console.error('Error resizing image:', error);
      //   });
      this.markers.push(marker);
    }
  }

  private getClusterPhotos(cluster: PostsCluster): Image[]{
    let photos: Image[] = [];

    for(let post of cluster.posts){
      photos.push(...post.photos);
    }
    return photos;
  }

  public onZoomChange(zoomLevel:number): void{
    console.log(zoomLevel);
    this.zoom = zoomLevel;
    this.updateMap(zoomLevel);
  }

  public getImageUrl(base64: string, fileName: string): any {
    return this.imageHelperService.getSanitizedBlobUrlFromBase64(base64,fileName);
  }

  // resizeImage(imageUrl: string, width: number, height: number): Promise<string> {
  //   return new Promise<string>((resolve, reject) => {
  //     const img = new Image();
  //     img.onload = () => {
  //       const canvas = document.createElement('canvas');
  //       canvas.width = width;
  //       canvas.height = height;
  //       const ctx = canvas.getContext('2d');
  //       ctx!.drawImage(img, 0, 0, width, height);
  //       const resizedImageUrl = canvas.toDataURL(); 
  //       resolve(resizedImageUrl);
  //     };
  //     img.onerror = reject;
  //     img.src = imageUrl;
  //   });
  // }

  public navigateToPosts(posts: MapPost[]):void{
    this._mapService.setPosts(posts);
    this.router.navigate(['navigation/posts/map']);
  }
}
