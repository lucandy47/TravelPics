import { DocumentService } from './../../services/api/document.service';
import { Component, ViewChild, OnInit, NgZone, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MapsAPILoader } from '@agm/core';

@Component({
  selector: 'travelpics-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss']
})
export class NewPostComponent implements OnInit {
  @ViewChild('search')
  public searchElementRef!: ElementRef;
  
  constructor(
    private _documentService:DocumentService,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {}
  selectedFiles: any[] = [];

  public file!: any;

  public newPostForm!: FormGroup;

  public latitude!: number;
  public longitude!: number;
  public address!: string | undefined;
  
  ngOnInit(): void {
    this.newPostForm = new FormGroup({
      description: new FormControl(null, [
      ]),
      photos: new FormControl(null,[]),
      location: new FormControl(null)
    });

    this.mapsAPILoader.load().then(() => {
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);

      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
  
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          this.address = place.formatted_address;
          // this.latitude = place.geometry.location!.lat();
          // this.longitude = place.geometry.location!.lng();
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

  public createPost(): void{
    // const formData = new FormData();
    // formData.append('file', this.file);

    // this._documentService.uploadPhoto(formData).subscribe({
    //   next: (data: any)=>{
    //     console.log('Photo uploaded successfully');
    //   },
    //   error: (error: any)=>{
    //     console.error('Error uploading photo:', error);
    //   }
    // });
  }
}
