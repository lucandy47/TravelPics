import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostRoutingModule } from './post-routing.module';
import { NewPostComponent } from './new-post/new-post.component';
import { SharedModule } from '../shared/shared.module';
import { PreviewPostComponent } from './preview-post/preview-post.component';
import { MyPostsComponent } from './my-posts/my-posts.component';
import { UserPostsComponent } from './user-posts/user-posts.component';
import { LocationPostsComponent } from './location-posts/location-posts.component';
import { MapPostsComponent } from './map-posts/map-posts.component';
import { PostComponent } from './post/post.component';
import { MostAppreciatedComponent } from './most-appreciated/most-appreciated.component';


@NgModule({
  declarations: [
    NewPostComponent,
    PreviewPostComponent,
    MyPostsComponent,
    UserPostsComponent,
    LocationPostsComponent,
    MapPostsComponent,
    PostComponent,
    MostAppreciatedComponent
  ],
  imports: [
    CommonModule,
    PostRoutingModule,
    SharedModule
  ]
})
export class PostModule { }
