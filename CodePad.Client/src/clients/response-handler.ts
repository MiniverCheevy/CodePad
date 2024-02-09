/* eslint-disable @typescript-eslint/no-explicit-any */
// This class is an extensibility point, use it to handle errors or any other post request chores

import { MessengerService } from ".";
import { Reply } from "./Reply";

export class ResponseHandler {
    returnFailure(e: any): any {
        MessengerService.incrementHttpRequestCounter();
        const reply = e as Reply;
        MessengerService.showToast(reply.message, true);
        return e.json();
    }
    returnSuccess(e: any): any {
        MessengerService.incrementHttpRequestCounter();
        const reply = e as Reply;
        MessengerService.showToast(reply.message, false);        
        return e.json();
    }


}

