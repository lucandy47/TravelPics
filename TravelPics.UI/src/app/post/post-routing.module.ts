import { NewPostComponent } from './new-post/new-post.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../services/ui/auth/guards/auth.guard';
import { MyPostsComponent } from './my-posts/my-posts.component';
import { UserPostsComponent } from './user-posts/user-posts.component';
import { LocationPostsComponent } from './location-posts/location-posts.component';
import { MapPostsComponent } from './map-posts/map-posts.component';
import { PostComponent } from './post/post.component';
import { MostAppreciatedComponent } from './most-appreciated/most-appreciated.component';

const routes: Routes = [
  {
    path:'new',
    canActivate: [AuthGuard],
    component: NewPostComponent,
  },
  {
    path:'my',
    canActivate: [AuthGuard],
    component: MyPostsComponent,
  },
  {
    path:'map',
    canActivate: [AuthGuard],
    component: MapPostsComponent,
  },
  {
    path:'location',
    canActivate: [AuthGuard],
    component: LocationPostsComponent,
  },
  {
    path:'most-appreciated',
    canActivate: [AuthGuard],
    component: MostAppreciatedComponent,
  },
  {
    path:'user/:id',
    canActivate: [AuthGuard],
    component: UserPostsComponent,
  },
  {
    path:':id',
    canActivate: [AuthGuard],
    component: PostComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }
