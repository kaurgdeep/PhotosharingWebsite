import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { IRegisterUser } from '../../Dtos/Interfaces/IRegisterUser';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-register-view',
  templateUrl: './register-view.component.html',
  styleUrls: ['./register-view.component.css']
})
export class RegisterViewComponent implements OnInit {

  apiCall: boolean;
  errorStatus: string;
  FB: any;

  constructor(private activatedRoute: ActivatedRoute, public router: Router, private userService: UserService ) { }

  ngOnInit() {

    (window as any).fbAsyncInit = function() {
      this.FB.init({
        appId      : '498293024296483',
        cookie     : true,
        xfbml      : true,
        version    : 'v3.1'
      });
        
      this.FB.AppEvents.logPageView();   
        
    };
  
    (function(d, s, id){
       var js, fjs = d.getElementsByTagName(s)[0];
       if (d.getElementById(id)) {return;}
       js = d.createElement(s); js.id = id;
       js.src = "https://connect.facebook.net/en_US/sdk.js";
       fjs.parentNode.insertBefore(js, fjs);
     }(document, 'script', 'facebook-jssdk'));
  }

  submitLogin(){
    console.log("submit login to facebook");
    // FB.login();
    this.FB.login((response: { authResponse: any; })=>
        {
          console.log('submitLogin',response);
          if (response.authResponse)
          {
            //login success
            //login success code here
            //redirect to home page
            this.router.navigate(['/profile']);
           }
           else
           {
           console.log('User login failed');
         }
      });

  }

  async register(user: IRegisterUser) {
    const response = await this.userService.create(user);
    console.log(response);
    if (response) {
      //this.status = 'Login succeeded! Redirecting to home page';
      setTimeout(() => {
        console.log('Redirecting to home', JSON.stringify(localStorage));
        this.router.navigate(['/profile']);
      }, 100);
    } else {
      this.apiCall = false;
      this.errorStatus = 'Login failed';
    }

    
    
  }

}
