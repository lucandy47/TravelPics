import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostRoutingModule } from './post-routing.module';
import { NewPostComponent } from './new-post/new-post.component';
import { SharedModule } from '../shared/shared.module';
import { PreviewPostComponent } from './preview-post/preview-post.component';
import { MyPostsComponent } from './my-posts/my-posts.component';


@NgModule({
  declarations: [
    NewPostComponent,
    PreviewPostComponent,
    MyPostsComponent
  ],
  imports: [
    CommonModule,
    PostRoutingModule,
    SharedModule
  ]
})
export class PostModule { }
