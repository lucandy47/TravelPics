import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostRoutingModule } from './post-routing.module';
import { NewPostComponent } from './new-post/new-post.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    NewPostComponent
  ],
  imports: [
    CommonModule,
    PostRoutingModule,
    SharedModule
  ]
})
export class PostModule { }
