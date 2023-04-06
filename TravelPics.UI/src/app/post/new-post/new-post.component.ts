import { DocumentService } from './../../services/api/document.service';
import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'travelpics-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss']
})
export class NewPostComponent {
  @ViewChild('fileInput', { static: false }) fileInput: any;
  constructor(private _documentService:DocumentService) {}


  onFileSelected() {
    const file = this.fileInput.nativeElement.files[0];
    const formData = new FormData();
    formData.append('file', file);

    this._documentService.uploadPhoto(formData).subscribe({
      next: (data: any)=>{
        console.log('Photo uploaded successfully');
      },
      error: (error: any)=>{
        console.error('Error uploading photo:', error);
      }
    });
  }
}
