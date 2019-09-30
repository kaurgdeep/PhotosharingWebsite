import { UserService } from "../../services/UserService";

export interface ICommentLike {
    commentId?: number;
    userId?: number;
}

export interface IComment {
    commentId?: number;
    commentLikes?: ICommentLike[];
    postId?: number;
    commenText?: string;
    createdAt?: string | Date;

}