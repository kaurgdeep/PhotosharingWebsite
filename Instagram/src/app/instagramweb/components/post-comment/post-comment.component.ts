import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IPost } from '../../Dtos/Interfaces/IPost';
import { PostService } from '../../services/PostService';
import { IComment } from '../../Dtos/Interfaces/IComment';
import { CommentService } from '../../services/CommentService';
import { environment } from '../../../../environments/environment';
import * as moment from 'moment';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-comment',
  templateUrl: './post-comment.component.html',
  styleUrls: ['./post-comment.component.css']
})
export class PostCommentComponent implements OnInit {
  @Input() postData: IPost;
  @Input() userId: number;
  apiUrl = environment.apiUrl;
  //Moment: moment.Moment = moment("someDate");

  me: IUserInformation;

	liked: boolean;
  vm: IComment = { };
  apiCall: boolean;

  constructor(private userService: UserService, private commentService: CommentService, private postService: PostService, public router: Router) { }

  async ngOnInit() {
    this.me = await this.userService.getMe();
    console.log(this.me);
    if (this.me == null) {
        this.router.navigate(['/login']);
    }
  }

  @Output() commentDone = new EventEmitter();
 
  momentTime(time: string) {
    return moment.utc(time).local().fromNow();
  
  }

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
    console.log(this.postData);
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
