import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

export abstract class ApiServiceBase {

    constructor(protected httpClient: HttpClient, private baseUrl: string) {
    }

    protected async httpPost<TRequest, TResponse>({
        url = '', payload }: { url?: string; payload?: TRequest; }): Promise<TResponse> {

        const headers: HttpHeaders = new HttpHeaders()
            .append('Content-Type', 'application/json');

        const request = new Promise<TResponse>((resolve, reject) => {
            this.httpClient.post<TResponse>(`${this.baseUrl}/${url}`, payload, { headers })
                // .retry(retries)
                .subscribe((data) => resolve(data), (reason) => reject(reason));
        });

        return await this.executePromise<TResponse>(request);
    }

    protected async httpGet<TResponse>({
        url = '' }: { url?: string } = {}, fromApi?: Function): Promise<TResponse> {
        const request = new Promise<TResponse>((resolve, reject) => {
            this.httpClient.get<TResponse>(`${this.baseUrl}/${url}`)
                // .retry(retries)
                .subscribe((data) => resolve(fromApi ? fromApi(data) : data), (reason) => reject(reason));
        });

        return await this.executePromise<TResponse>(request);
    }

    protected async httpGetQueryParams<TRequest, TResponse>({
        url = '', queryParams, defaultResponse = {} }: { url?: string, queryParams: TRequest, defaultResponse: any }): Promise<TResponse> {

        // Note: Object.entries shows as missing in VSCode - due to some issue with VSCode not picking up latest definitions
        const params = Object.entries(queryParams).reduce((params, [key, value]) => params.set(key, value), new HttpParams());

        const request = new Promise<TResponse>((resolve, reject) => {
            this.httpClient.get<TResponse>(`${this.baseUrl}/${url}`, { params })
                // .retry(retries)
                .subscribe((data) => resolve(data), (reason) => reject(reason));
        });

        return await this.executePromise<TResponse>(request, defaultResponse);
    }

    private async executePromise<T>(promise: Promise<T>, defaultResponse: any = {}) {
        // let response: T; // todo: till we figure out how to get default value of T (which seems to be impossible in TypeScript)
        let response;

        try {
            response = await promise;

            // note: workaround when a successful response returns null or undefined 
            if (!response) {
                // todo: if response type is of type array, return en empty array [] instead of {}
                response = defaultResponse;
            }
        } catch (e) {
            console.error(e);
            // non 200 response
            // todo: handle 400 by reading the validation errors from the response
        }

        return response;
    }

    protected async httpDelete<TResponse>({
        url = '' }: { url?: string } = {}): Promise<TResponse> {
        const request = new Promise<TResponse>((resolve, reject) => {
            this.httpClient.delete<TResponse>(`${this.baseUrl}/${url}`)
                // .retry(retries)
                .subscribe((data) => resolve(data), (reason) => reject(reason));
        });

        return await this.executePromise<TResponse>(request);
    }

}
