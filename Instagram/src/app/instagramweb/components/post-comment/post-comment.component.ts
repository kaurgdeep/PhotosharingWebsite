import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IPost } from '../../Dtos/Interfaces/IPost';
import { PostService } from '../../services/PostService';
import { IComment } from '../../Dtos/Interfaces/IComment';
import { CommentService } from '../../services/CommentService';

@Component({
  selector: 'app-post-comment',
  templateUrl: './post-comment.component.html',
  styleUrls: ['./post-comment.component.css']
})
export class PostCommentComponent implements OnInit {
  @Input() postData: IPost;
  @Input() userId: number;

	liked: boolean;
  vm: IComment = { };
  apiCall: boolean;

  constructor(private commentService: CommentService, private postService: PostService) { }

  ngOnInit() {
  }

  @Output() commentDone = new EventEmitter();
 

  async commentClick() {
    this.apiCall = true;
    this.vm.postId = this.postData.postId;
    await this.commentService.create(this.vm);
    //this.postDone.emit(this.vm);

    this.vm = { };
    this.apiCall = false;
  }

  // async likeClick(postId: number) {
  //   this.apiCall = true;
  //   await this.postService.createPostLike(postId);
    
  // }

  likedByMe() {
    if (!this.postData || !this.postData.postLikes || this.postData.postLikes.length == 0) {
        return this.liked;
    } 
    return this.postData.postLikes.filter(x => x.userId == this.userId).length != 0 || this.liked;
}

likeCount() {
    return ((this.postData && this.postData.postLikes) ? this.postData.postLikes.length : 0);
}

async likeClick(postId: number) {
    this.apiCall = true;
    await this.postService.createPostLike(postId);
    this.apiCall = false;
    this.liked = true;
}

async unlikeClick(postId: number) {
    this.apiCall = true;
    await this.postService.deletePostLike(postId);
    this.apiCall = false;
    this.liked = false;
}



}
