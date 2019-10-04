import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CredentialsComponent } from './instagramweb/components/credentials/credentials.component';
import { FormsModule } from '@angular/forms';
import { LoginViewComponent } from './instagramweb/views/login-view/login-view.component';
import { RegisterViewComponent } from './instagramweb/views/register-view/register-view.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseUrlHttpInterceptor } from './instagramweb/services/BaseUrlHttpInterceptor';
import { UserService } from './instagramweb/services/UserService';
import { AuthenticationStore } from './instagramweb/services/AuthenticationStore';
import { AuthenticationHttpInterceptor } from './instagramweb/services/AuthenticationHttpInterceptor';
import { AuthenticationGuardService } from './instagramweb/services/AuthenticationGuardService';
import { HomeViewComponent } from './instagramweb/views/home-view/home-view.component';
import { ProfileViewComponent } from './instagramweb/views/profile-view/profile-view.component';
import { TimelineViewComponent } from './instagramweb/views/timeline-view/timeline-view.component';
import { PostComponent } from './instagramweb/components/post/post.component';
import { PostsListComponent } from './instagramweb/components/posts-list/posts-list.component';
import { PostService } from './instagramweb/services/PostService';
import { PostCommentComponent } from './instagramweb/components/post-comment/post-comment.component';
import { CommentService } from './instagramweb/services/CommentService';
import { CommentListComponent } from './instagramweb/components/comment-list/comment-list.component';
import { UserComponent } from './instagramweb/components/user/user.component';
import { CommentComponent } from './instagramweb/components/comment/comment.component';
import { UserListComponent } from './instagramweb/components/user-list/user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    CredentialsComponent,
    LoginViewComponent,
    RegisterViewComponent,
    HomeViewComponent,
    ProfileViewComponent,
    TimelineViewComponent,
    PostComponent,
    PostsListComponent,
    PostCommentComponent,
    CommentListComponent,
    UserComponent,
    CommentComponent,
    UserListComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    UserService,
    PostService,
    CommentService,
    AuthenticationStore,
    AuthenticationGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
