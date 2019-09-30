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
  @Input() commentData: IComment;
  @Input() userId: number;
  apiCall: boolean;
  liked: boolean;
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

  likedByMe() {
    console.log(this.commentData);
    if (!this.commentData || !this.commentData.commentLikes || this.commentData.commentLikes.length == 0) {
        return this.liked;
    } 
    return this.commentData.commentLikes.filter(x => x.userId == this.userId).length != 0 || this.liked;
  }

  likeCount() {
      return ((this.commentData && this.commentData.commentLikes) ? this.commentData.commentLikes.length : 0);
  }

  async likeClick(commentId: number) {
      this.apiCall = true;
      await this.postService.createCommentLike(commentId);
      this.apiCall = false;
      this.liked = true;
  }

  async unlikeClick(commentId: number) {
      this.apiCall = true;
      await this.postService.deleteCommentLike(commentId);
      this.apiCall = false;
      this.liked = false;
  }

}
