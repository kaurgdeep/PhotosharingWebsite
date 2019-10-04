import { IUserFriend } from './IUserFriend';
export interface IUserInformation {
    userId?: number;
    emailAddress?: string;
    firstName?: string;
    lastName?: string;
    userFriends?: IUserFriend[];
    
}
