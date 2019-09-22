import { Component, OnInit, Input } from '@angular/core';
import { IComment } from '../../Dtos/Interfaces/IComment';
import { CommentService } from '../../services/CommentService';
import { PostService } from '../../services/PostService';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
@Input() postId : number;
  comments: IComment[] = [];
  skip = 0;
  take = 25;

  constructor(private postService: PostService) { }

  async ngOnInit() {
    this.comments = await this.postService.getComments(this.postId, this.skip, this.take);
  }

  async getMore() {
    this.skip += this.take;

    this.comments = this.comments.concat(await this.postService.getComments(this.postId, this.skip, this.take));

  }

}
