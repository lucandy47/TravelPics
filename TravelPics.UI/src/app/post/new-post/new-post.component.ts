import { DocumentService } from './../../services/api/document.service';
import { Component, ViewChild, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'travelpics-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss']
})
export class NewPostComponent implements OnInit {
  @ViewChild('fileInput', { static: false }) fileInput: any;
  constructor(private _documentService:DocumentService) {}
  selectedFiles: any[] = [];

  public file!: any;

  public newPostForm!: FormGroup;

  ngOnInit(): void {
    this.newPostForm = new FormGroup({
      description: new FormControl(null, [
      ]),
      photos: new FormControl(null,[]),
      location: new FormControl(null)
    });
  }

  onFileSelected() {
    this.file = this.fileInput.nativeElement.files[0];
    const formData = new FormData();
    formData.append('file', this.file);

    this._documentService.uploadPhoto(formData).subscribe({
      next: (data: any)=>{
        console.log('Photo uploaded successfully');
      },
      error: (error: any)=>{
        console.error('Error uploading photo:', error);
      }
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

  }
}