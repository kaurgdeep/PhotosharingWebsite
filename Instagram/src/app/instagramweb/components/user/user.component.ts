import { Component, OnInit } from '@angular/core';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  apiCall: boolean;
  users: IUserInformation[] = [];
  userId: number;
  skip = 0;
  take = 25;
  constructor(private userService: UserService) { }

  async getMore() {
    this.skip += this.take;

    this.users = this.users.concat(await this.userService.getMany(this.skip, this.take));
  }
  async ngOnInit() {
    var me = await this.userService.getMe();
    if (!me) {
        console.error("Unable to get me");
        return;
    }
    this.userId = me.userId;
    this.users = await this.userService.getMany(this.skip, this.take);

  }

  // addedByMe() {
  //   //console.log(this.postData);
  //   if (!this.postData || !this.postData.postLikes || this.postData.postLikes.length == 0) {
  //       return this.liked;
  //   } 
  //   return this.postData.postLikes.filter(x => x.userId == this.userId).length != 0 || this.liked;
  // }

  async friendClick(friendId: number) {
    this.apiCall = true;
    await this.userService.createAddFriend(friendId);
    this.apiCall = false;
   
  }

  async unFriendClick(friendId: number) {
    this.apiCall = true;
    await this.userService.deleteAddFriend(friendId);
    this.apiCall = false;
  }

}
