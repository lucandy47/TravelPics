import { NewPostComponent } from './new-post/new-post.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../services/ui/auth/guards/auth.guard';
import { MyPostsComponent } from './my-posts/my-posts.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }
