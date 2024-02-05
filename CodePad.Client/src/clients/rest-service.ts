/* eslint-disable @typescript-eslint/no-explicit-any */
import { EncoderService, RestRequest } from "./";

class RestServicePrototype {
    private buildFetchRequest = (restRequest:RestRequest) => {
        const requestInit = restRequest.requestHandler.getRequestInit(restRequest);
        const updatedUrl = restRequest.requestHandler.updateUrl(restRequest.url);
                return fetch(updatedUrl,
                    requestInit
                )
                .then((r) => { return restRequest.responseHandler.returnSuccess(r) })
                .catch((e) => { return restRequest.responseHandler.returnFailure(e) });
    }

    public buildGetRequest = async (restRequest:RestRequest): Promise<any> => {        
        const params = EncoderService.serializeParams(restRequest.request);
        if (params != '')
            restRequest.url = restRequest.url + '?' + params;
        return this.buildFetchRequest(restRequest);
    }

    public buildPutRequest = async (restRequest:RestRequest): Promise<any> => {       
        const req = await this.buildFetchRequest(restRequest);
        return req;
    }

    public buildPostRequest = async (restRequest:RestRequest): Promise<any> => {
        const req = await this.buildFetchRequest(restRequest);
        return req;
    }

    public buildPatchRequest = async (restRequest:RestRequest): Promise<any> => {
        const req = await this.buildFetchRequest(restRequest);
        return req;
    }

    public buildDeleteRequest = async (restRequest:RestRequest): Promise<any> => {
        const params = EncoderService.serializeParams(restRequest.request);
        if (params != '')
            restRequest.url = restRequest.url + '?' + params;
        return this.buildFetchRequest(restRequest);
    }
}

export const RestService = new RestServicePrototype();
