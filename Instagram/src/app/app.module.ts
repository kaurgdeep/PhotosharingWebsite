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

@NgModule({
  declarations: [
    AppComponent,
    CredentialsComponent,
    LoginViewComponent,
    RegisterViewComponent,
    HomeViewComponent,
    ProfileViewComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    UserService,
    AuthenticationStore,
    AuthenticationGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
