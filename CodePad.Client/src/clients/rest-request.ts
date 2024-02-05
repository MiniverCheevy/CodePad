import { RequestHandler, ResponseHandler } from "./";
export class RestRequest {
    url: string = '';
    verb: string = '';
    request: any;
    requestHandler = new RequestHandler();
    responseHandler = new ResponseHandler();
}