import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationComponent } from './navigation/navigation.component';
import { AuthGuard } from '../services/ui/auth/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { PostModule } from '../post/post.module';
import { ProfileModule } from '../profile/profile.module';

const routes: Routes = [
  {
    path:'',
    pathMatch:'full',
    redirectTo:'navigation/home'
  },
  {
    path:'',
    component:NavigationComponent,
    data: {baseRoute: 'navigation'},
    children: [
      {
        path:'home',
        canActivate:[AuthGuard],
        component:HomeComponent
      },
      {
        path: 'posts',
        canActivate:[AuthGuard],
        loadChildren: () => PostModule,
      },
      {
        path:'profile',
        canActivate:[AuthGuard],
        loadChildren: () => ProfileModule
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NavigationRoutingModule { }
