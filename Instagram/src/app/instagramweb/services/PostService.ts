import { Injectable, SkipSelf } from "@angular/core";
import { ApiServiceBase } from "./ApiServiceBase";
import { HttpClient } from "@angular/common/http";
import { ICreateResponse } from '../Dtos/Interfaces/ICreateResponse';
import { IPost } from '../Dtos/Interfaces/IPost';
import { IComment } from "../Dtos/Interfaces/IComment";
import { Post } from "../Dtos/Entities/Post";



@Injectable()
export class PostService extends ApiServiceBase {
    constructor(httpClient: HttpClient) {
        super(httpClient, '/api/posts');
    }

    async create(post: IPost): Promise<ICreateResponse> {
        return await super.httpPost<IPost, ICreateResponse>({ payload: post });
    }

    async getMany(skip : number, take: number ) : Promise<IPost[]>{
        var qs = `?skip=${skip}&take=${take}`;
        return await super.httpGet<IPost[]>({url:qs}, Post.fromApiArray);

    }

    
    async getComments(postId: number, skip : number, take: number ) : Promise<IComment[]>{
        var qs = `${postId}/comments?skip=${skip}&take=${take}`;
        return await super.httpGet<IComment[]>({url:qs});

    }

    async createPostLike(postId: number): Promise<ICreateResponse> {
        return await super.httpPost<number, ICreateResponse>({ url: `${postId}/like` });
    }

    async deletePostLike(postId: number): Promise<{}> {
        return await super.httpDelete<{}>({ url: `${postId}/like` });
    }

    async createCommentLike(commentId: number): Promise<ICreateResponse> {
        return await super.httpPost<number, ICreateResponse>({ url: `${commentId}/like` });
    }

    async deleteCommentLike(commentId: number): Promise<{}> {
        return await super.httpDelete<{}>({ url: `${commentId}/like` });
    }


    


    // likePost(postId: number) { httpPost..({url: `{postId}/like`})}
    // unlikePost(postId: number) { httpDelete..({url: `{postId}/like`})}

}