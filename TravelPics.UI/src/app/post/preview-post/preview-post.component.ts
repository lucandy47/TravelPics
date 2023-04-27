import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { NewPost } from 'src/app/services/api/dtos/new-post';
import { PostImage } from 'src/app/services/api/dtos/post-image';
import { GaleriaResponsiveOptions } from 'src/app/shared/utils/galeria-options';

@Component({
  selector: 'travelpics-preview-post',
  templateUrl: './preview-post.component.html',
  styleUrls: ['./preview-post.component.scss']
})
export class PreviewPostComponent implements OnInit {

  public responsiveOptions: any[] = [];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ){}

  public post!:NewPost;
  public userName!: string;
  
  public images: PostImage[] = [];

  ngOnInit(): void {
    this.post = this.config.data?.post;
    this.userName = this.config.data?.userName;
    this.images = this.config.data?.images;

    this.responsiveOptions = GaleriaResponsiveOptions.responsiveOptions;
  }

}
