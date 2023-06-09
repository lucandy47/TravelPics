import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { WelcomePageComponent } from './welcome-page/welcome-page.component';
import { NavigationModule } from './navigation/navigation.module';
import { MapComponent } from './shared/map/map.component';
import { AuthGuard } from './services/ui/auth/guards/auth.guard';

const routes: Routes = [
  {
    path:'',
    component:WelcomePageComponent
  },
  {
    path:'map',
    component:MapComponent
  },
  {
    path:'navigation',
    canActivate: [AuthGuard],
    loadChildren: () => NavigationModule
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
    path:'**',
    component: PageNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
