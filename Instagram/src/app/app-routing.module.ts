import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginViewComponent } from './instagramweb/views/login-view/login-view.component';
import { RegisterViewComponent } from './instagramweb/views/register-view/register-view.component';
import { AuthenticationGuardService } from './instagramweb/services/AuthenticationGuardService';
import { HomeViewComponent } from './instagramweb/views/home-view/home-view.component';
import { ProfileViewComponent } from './instagramweb/views/profile-view/profile-view.component';
import { TimelineViewComponent } from './instagramweb/views/timeline-view/timeline-view.component';






const routes: Routes = [
  { path: '', component: RegisterViewComponent },
  { path: 'home', component: HomeViewComponent, canActivate: [AuthenticationGuardService] },

  { path: 'login', component: LoginViewComponent },
  { path: 'register', component: RegisterViewComponent },
  { path: 'profile', component: ProfileViewComponent, canActivate: [AuthenticationGuardService] },
  { path: 'timeline', component: TimelineViewComponent, canActivate: [AuthenticationGuardService] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
