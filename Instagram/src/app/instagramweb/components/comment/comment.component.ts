import { Component, OnInit, Input } from '@angular/core';
import { IComment } from '../../Dtos/Interfaces/IComment';
import { PostService } from '../../services/PostService';
import { CommentService } from '../../services/CommentService';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() commentData: IComment;
  @Input() userId: number;
  liked: boolean;
  apiCall: boolean;
  
  constructor(private commentService: CommentService) { }

  ngOnInit() {
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
      await this.commentService.createCommentLike(commentId);
      this.apiCall = false;
      this.liked = true;
  }

  async unlikeClick(commentId: number) {
      this.apiCall = true;
      await this.commentService.deleteCommentLike(commentId);
      this.apiCall = false;
      this.liked = false;
  }

}
