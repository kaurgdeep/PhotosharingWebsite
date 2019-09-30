import { UserService } from "../../services/UserService";

export interface IPostLike {
    postId?: number;
    userId?: number;
}

export interface IPost {
    postId?: number;
    postText?: string;
    imagePath?: string;
    postLikes?: IPostLike[];
    createdAt?: string | Date;

}
