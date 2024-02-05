/* eslint-disable @typescript-eslint/no-explicit-any */
// This class is an extensibility point, use it to handle errors or any other post request chores
export class ResponseHandler {
    returnFailure(e: any): any {
        throw new Error("Method not implemented.");
    }
    returnSuccess(r: Response): any {
        throw new Error("Method not implemented.");
    }


}

