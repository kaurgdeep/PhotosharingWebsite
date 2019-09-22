import { Injectable, SkipSelf } from "@angular/core";
import { ApiServiceBase } from "./ApiServiceBase";
import { HttpClient } from "@angular/common/http";
import { ICreateResponse } from '../Dtos/Interfaces/ICreateResponse';
import { IComment } from "../Dtos/Interfaces/IComment";



@Injectable()
export class CommentService extends ApiServiceBase {
    constructor(httpClient: HttpClient) {
        super(httpClient, '/api/comments');
    }

    async create(comment: IComment): Promise<ICreateResponse> {
        return await super.httpPost<IComment, ICreateResponse>({ payload: comment });
    }

    async getMany(skip : number, take: number ) : Promise<IComment[]>{
        var qs = `?skip=${skip}&take=${take}`;
        return await super.httpGet<IComment[]>({url:qs});

    }

    // likePost(postId: number) { httpPost..({url: `{postId}/like`})}
    // unlikePost(postId: number) { httpDelete..({url: `{postId}/like`})}

}