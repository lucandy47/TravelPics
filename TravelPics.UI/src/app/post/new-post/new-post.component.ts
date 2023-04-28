import { PostService } from '../../services/api/post.service';
import { Component, ViewChild, OnInit, NgZone, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MapsAPILoader } from '@agm/core';
import { Location } from 'src/app/services/api/dtos/location';
import { AuthUserService } from 'src/app/services/ui/auth/auth-user.service';
import { UserInfo } from 'src/app/services/ui/auth/user-info';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PreviewPostComponent } from '../preview-post/preview-post.component';
import { PostImage } from 'src/app/services/api/dtos/post-image';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { NewPost } from 'src/app/services/api/dtos/new-post';

@Component({
  selector: 'travelpics-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss'],
  providers: [DialogService]
})
export class NewPostComponent implements OnInit, OnDestroy {
  @ViewChild('search')
  public searchElementRef!: ElementRef;
  
  ref!: DynamicDialogRef;

  constructor(
    private _postService:PostService,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone,
    private _authUserService: AuthUserService,
    public dialogService: DialogService,
    private messageService: MessageService,
    private router: Router,
  ) {}

  ngOnDestroy(): void {
    if(this.ref){
      this.ref.close();
    }
  }
  selectedFiles: any[] = [];

  public newPostForm!: FormGroup;

  public address!: string | undefined;
  public location!: Location;
  
  public loggedInUser!: UserInfo;

  ngOnInit(): void {
    this.newPostForm = new FormGroup({
      description: new FormControl(null, [
      ]),
      photos: new FormControl(null,[]),
      location: new FormControl(null, [Validators.required])
    });

    this.mapsAPILoader.load().then(() => {
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);

      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
  
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          console.log(place);
          this.location = {
            name: place.name,
            address: place.formatted_address?.trim(),
            latitude: place.geometry.location!.lat(),
            longitude: place.geometry.location!.lng()
          };
        });
      });
    });

    this.loggedInUser = this._authUserService.loggedInUser;
  }

  public selectFiles(event: any): void {
    if(event.currentFiles.length > 0){
      this.selectedFiles = event.currentFiles;
    }
  }

  public removeAllFiles(): void{
    this.selectedFiles = [];
  }

  public removeSelectedFile(event: any): void{
    if(this.selectedFiles.length === 1){
      this.selectedFiles = [];
    }
  }

  public createPost(): void {
    const formData = new FormData();
    formData.append('Location', JSON.stringify(this.location));
    formData.append('Description', this.newPostForm.get('description')!.value);
    formData.append('CreatedById', this.loggedInUser.userId.toString());
  
    for (let i = 0; i < this.selectedFiles.length; i++) {
      formData.append(`Photos[${i}]`, this.selectedFiles[i]);
    }

    this._postService.addNewPost(formData).subscribe({
      next: (data: any) => {
        this.messageService.add({
          severity: 'success',
          summary: 'New Post',
          detail: 'Your post has been published!',
        });
        this.router.navigate(['home']);
      },
      error: (error: any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'New Post',
          detail: 'Could not publish new post.',
        });
      }
    });
  }

  public async previewPost(): Promise<void>{
    const formData = this.newPostForm.getRawValue();
    let post: NewPost = {
      description: formData.description,
      location: this.location,
      photos: this.selectedFiles,
      createdById: this.loggedInUser.userId
    }

    let images = await this.loadImages(post);

    this.ref = this.dialogService.open(PreviewPostComponent,{
      data: {
        post: post,
        userName: this.loggedInUser.name,
        images: images
      },
      header: "Preview Post",
      width: '660px',
      height: '550px',
      baseZIndex: 10000
    });
  }
  
  private async loadImages(post: NewPost): Promise<PostImage[]> {
    const images: PostImage[] = [];
    for (let i = 0; i < post.photos.length; i++) {
      const imageDataUrl = await this.readFileAsDataURL(post.photos[i]);
      images.push({
        itemImageSrc: imageDataUrl,
        thumbnailImageSrc: imageDataUrl
      });
    }
    return images;
  }
  
  private readFileAsDataURL(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => {
        resolve(reader.result as string);
      };
      reader.onerror = () => {
        reject(reader.error);
      };
      reader.readAsDataURL(file);
    });
  }
  
}
