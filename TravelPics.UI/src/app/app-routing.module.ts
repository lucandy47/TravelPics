import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { HomeComponent } from './home/home.component';
import { PostModule } from './post/post.module';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'auth',
    loadChildren: () => AuthModule,
  },
  {
    path: 'user',
    loadChildren: () => UserModule,
  },
  {
    path: 'posts',
    loadChildren: () => PostModule,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
