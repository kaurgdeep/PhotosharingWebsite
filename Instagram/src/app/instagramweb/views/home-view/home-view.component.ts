import { Component, OnInit } from '@angular/core';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-view',
  templateUrl: './home-view.component.html',
  styleUrls: ['./home-view.component.css']
})
export class HomeViewComponent implements OnInit {

  me: IUserInformation;

  constructor(private userService: UserService,public router: Router) { }

  async ngOnInit() {
    this.me = await this.userService.getMe();
    console.log(this.me);
    if (this.me == null) {
        this.router.navigate(['/login']);
    }
  }
  
}
