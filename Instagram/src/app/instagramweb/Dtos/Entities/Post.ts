import { IPost } from "../Interfaces/IPost";
import { parseTwoDigitYear } from "moment";

export class Post implements IPost {
    postId: number;
    postText: string;
    imagePath: string;

    //postLikes?: IPostLike[];
    createdAt: string | Date;

    static fromApi(post: IPost): IPost { 
        // var clonedObj = deepclone(post); // lodash has deepclone
        // clonedObj.createdAt = new Date(post.createdAt);
        // return clonedObj;

        return { 
            postId: post.postId,
            postText: post.postText,
            imagePath: post.imagePath,
            postLikes: post.postLikes,
            createdAt: new Date(post.createdAt)
        };
    }

    static fromApiArray(posts: IPost[]): IPost[] { 
        return posts.map(Post.fromApi);
        //return posts.map(post => Post.fromApi(post));
    }
}