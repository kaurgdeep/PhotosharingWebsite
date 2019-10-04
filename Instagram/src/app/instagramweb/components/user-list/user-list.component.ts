import { Component, OnInit } from '@angular/core';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: IUserInformation[] = [];
  userId: number;
  added: boolean;
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

  

}
