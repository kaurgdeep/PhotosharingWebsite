import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ILoginUser } from '../Dtos/Interfaces/ILoginUser';
import { AuthenticationStore } from './AuthenticationStore';
import { ITokenResponse } from '../Dtos/Interfaces/ITokenResponse';
import { IUserInformation } from '../Dtos/Interfaces/IUserInformation';
import { ApiServiceBase } from './ApiServiceBase';
import { IRegisterUser } from '../Dtos/Interfaces/IRegisterUser';
import { ICreateResponse } from '../Dtos/Interfaces/ICreateResponse';

@Injectable()
export class UserService extends ApiServiceBase {

    constructor(httpClient: HttpClient, private authenticationStore: AuthenticationStore) {
        super(httpClient, '/api/users');
    }

    async login(user: ILoginUser): Promise<boolean> {
        const authenticationToken = await super.httpPost<ILoginUser, ITokenResponse>({
            url: 'log-in',
            payload: user
        });

        return this.storeToken(authenticationToken);
        
    }

    private storeToken(authenticationToken: ITokenResponse){
        if (authenticationToken &&
            authenticationToken.token &&
            authenticationToken.token.length > 0) {

            this.authenticationStore.setAuthentication(authenticationToken.token);
            return true;
        }

        this.logout();
        return false;
    }

    async create(user: IRegisterUser): Promise<boolean> {
        const authenticationToken = await super.httpPost<IRegisterUser, ITokenResponse>({ url: 'register', payload: user});
        return this.storeToken(authenticationToken);
    }

    async getMe(): Promise<IUserInformation> {
        return super.httpGet({ url: 'me' });
    }

    logout(): void {
        this.authenticationStore.resetAuthentication();
    }
}
