import { Component, OnInit, Input } from '@angular/core';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() userData: IUserInformation;
  @Input() userId: number;
  added: boolean;
  apiCall: boolean;

  
  constructor(private userService: UserService) { }

  async ngOnInit() {

  }
  

  addedByMe() {
    console.log(this.userData);
    if (!this.userData || !this.userData.userFriends || this.userData.userFriends.length == 0) {
        return this.added;
    } 
    return this.userData.userFriends.filter(x => x.userId == this.userId).length != 0 || this.added;
  }

  async friendClick(friendId: number) {
    this.apiCall = true;
    await this.userService.createAddFriend(friendId);
    this.apiCall = false;
    this.added = true;
   
  }

  async unFriendClick(friendId: number) {
    this.apiCall = true;
    await this.userService.deleteAddFriend(friendId);
    this.apiCall = false;
    this.added = false;
  }

}
