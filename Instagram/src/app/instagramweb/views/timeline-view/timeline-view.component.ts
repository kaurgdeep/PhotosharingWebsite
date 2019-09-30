import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/PostService';
import { Router } from '@angular/router';
import { UserService } from '../../services/UserService';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';


@Component({
  selector: 'app-timeline-view',
  templateUrl: './timeline-view.component.html',
  styleUrls: ['./timeline-view.component.css']
})
export class TimelineViewComponent implements OnInit {

  me: IUserInformation;

  constructor(private userService: UserService,public router: Router) { }

  async ngOnInit() {
    this.me = await this.userService.getMe();
    console.log(this.me);
    if (this.me == null) {
        this.router.navigate(['/login']);
    }
  }

  postDone(post) {
    // add the newly create post to the beggining of the list
    //this.posts = 
  }


}
