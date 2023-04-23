import { PostService } from '../../services/api/post.service';
import { Component, ViewChild, OnInit, NgZone, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MapsAPILoader } from '@agm/core';
import { Location } from 'src/app/services/api/dtos/location';
import { Post } from 'src/app/services/api/dtos/post';

@Component({
  selector: 'travelpics-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss']
})
export class NewPostComponent implements OnInit {
  @ViewChild('search')
  public searchElementRef!: ElementRef;
  
  constructor(
    private _postService:PostService,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {}
  selectedFiles: any[] = [];

  public newPostForm!: FormGroup;

  public address!: string | undefined;
  public location!: Location;
  
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
    formData.append('CreatedById', '1');
  
    for (let i = 0; i < this.selectedFiles.length; i++) {
      formData.append(`Photos[${i}]`, this.selectedFiles[i]);
    }

    this._postService.uploadPhoto(formData).subscribe({
      next: (data: any) => {
        console.log('Post successfully');
      },
      error: (error: any) => {
        console.error('Error adding post:', error);
      }
    });
  }
}
