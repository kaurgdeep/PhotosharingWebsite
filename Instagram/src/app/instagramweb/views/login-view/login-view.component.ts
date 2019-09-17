import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IRegisterUser } from '../../Dtos/Interfaces/IRegisterUser';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-login-view',
  templateUrl: './login-view.component.html',
  styleUrls: ['./login-view.component.css']
})
export class LoginViewComponent implements OnInit {

  status: string;
  errorStatus: string;
  apiCall: boolean;

  constructor(private activatedRoute: ActivatedRoute, public router: Router, private userService: UserService) { }

  ngOnInit() {

    this.userService.logout();

    }

  async login(user: IRegisterUser) {
    console.log(user);
    this.apiCall = true;
    const response = await this.userService.login(user);
    if (response) {
      this.status = 'Login succeeded! Redirecting to home page';
      setTimeout(() => {
        console.log('Redirecting to home', JSON.stringify(localStorage));
        this.router.navigate(['/profile']);
      }, 100);
    } else {
      this.apiCall = false;
      this.errorStatus = 'Login failed';
    }
    //console.log(user);
  }

}
