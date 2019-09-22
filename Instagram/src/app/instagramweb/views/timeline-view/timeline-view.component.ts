import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/PostService';


@Component({
  selector: 'app-timeline-view',
  templateUrl: './timeline-view.component.html',
  styleUrls: ['./timeline-view.component.css']
})
export class TimelineViewComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  postDone(post) {
    // add the newly create post to the beggining of the list
    //this.posts = 
  }


}
