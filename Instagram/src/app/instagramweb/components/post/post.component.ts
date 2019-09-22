import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { PostService } from '../../services/PostService';
import { IPost } from '../../Dtos/Interfaces/IPost';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  vm: IPost = {};
  apiCall: boolean;

  constructor(private postService: PostService) { }

  ngOnInit() {
  }

  @Output() postDone = new EventEmitter();
  

  async postClick() {
    this.apiCall = true;
    await this.postService.create(this.vm); // POST api/posts
    //this.postDone.emit(this.vm);

    this.vm = { };
    this.apiCall = false;

    
  }


}
