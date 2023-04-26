import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Post } from 'src/app/services/api/dtos/post';
import { PostImage } from 'src/app/services/api/dtos/post-image';

@Component({
  selector: 'travelpics-preview-post',
  templateUrl: './preview-post.component.html',
  styleUrls: ['./preview-post.component.scss']
})
export class PreviewPostComponent implements OnInit {

  public responsiveOptions: any[] = [
    {
        breakpoint: '1024px',
        numVisible: 5
    },
    {
        breakpoint: '768px',
        numVisible: 3
    },
    {
        breakpoint: '560px',
        numVisible: 1
    }
  ];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ){}

  public post!:Post;
  public userName!: string;
  
  public images: PostImage[] = [];

  ngOnInit(): void {
    this.post = this.config.data?.post;
    this.userName = this.config.data?.userName;
    this.images = this.config.data?.images;
  }

}
