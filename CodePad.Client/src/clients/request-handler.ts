// This class is an extensibility point, use it to handle authorization or any other pre request chores
// This class is for global settings each client will have a custom request handler for api specific settings, such as base url

import { RestRequest } from "./";
import { MessengerService } from "./"
export class RequestHandler {
    getAuthToken() {
        //Get auth token from wherever it's stored;
        return '';
    }
    updateUrl(url: string) {
        //default to your dev environment and then pull environment specific urls based on build
        //like process.env.MyApi_ENDPOINT
        return `https://localhost:9996/${url}`;
    }
    getHeaders(): Record<string, string> {
        const authToken = this.getAuthToken();
        return {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',
            'Origin': window.location.origin,
            'Authorization': `Bearer ${authToken}`,
        };
    }
    getRequestInit(restRequest: RestRequest) {
        MessengerService.incrementHttpRequestCounter();
        if (restRequest.verb == "GET" || restRequest.verb == "DELETE")
            return this.getUrlRequest(restRequest);
        else
            return this.getBodyRequest(restRequest);
    }
    getUrlRequest(restRequest: RestRequest): RequestInit {
        
        const headers = this.getHeaders();
        return {
            mode: 'cors',
            method: restRequest.verb,
            //credentials: 'include',
            headers: headers
        } as RequestInit;
    }
    getBodyRequest(restRequest: RestRequest): RequestInit {
        const headers = this.getHeaders();
        return {
            mode: 'cors',
            method: restRequest.verb,
            //credentials: 'include',
            headers: headers,
            body: JSON.stringify(restRequest.request)
        } as RequestInit;
    }

}
