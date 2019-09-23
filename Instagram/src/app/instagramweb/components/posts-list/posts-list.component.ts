import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/PostService';
import { IPost } from '../../Dtos/Interfaces/IPost';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent implements OnInit {

  posts: IPost[] = [];
  userId: number;
  skip = 0;
  take = 25;

  constructor(private postService: PostService, private userService: UserService) { }

  async getMore() {
    this.skip += this.take;

    this.posts = this.posts.concat(await this.postService.getMany(this.skip, this.take));

  }

  async ngOnInit() {
    var me = await this.userService.getMe();
    if (!me) {
        console.error("Unable to get me");
        return;
    }
    this.userId = me.userId;
    this.posts = await this.postService.getMany(this.skip, this.take);
}


}
