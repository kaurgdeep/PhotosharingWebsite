import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/PostService';
import { IPost } from '../../Dtos/Interfaces/IPost';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent implements OnInit {

  posts: IPost[] = [];
  skip = 0;
  take = 25;

  constructor(private postService: PostService) { }

  async ngOnInit() {
    this.posts = await this.postService.getMany(this.skip, this.take);
  }

  async getMore() {
    this.skip += this.take;

    this.posts = this.posts.concat(await this.postService.getMany(this.skip, this.take));

  }

}
