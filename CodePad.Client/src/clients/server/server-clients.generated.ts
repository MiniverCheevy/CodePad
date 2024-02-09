

//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************



import * as models from './server-models.generated';
import { ServerRequestHandler } from './server-request-handler';
import { ServerResponseHandler } from './server-response-handler';
import { RestRequest,RestService } from '../';


export class FormatClient {
public static Format (command: models.FormatCommand) : Promise<models.FormatResult> {
    const url = `api/FormatTemplates/format`;
    const restRequest:RestRequest = { 
        url:url,
        verb: 'POST',
        request:command,
        requestHandler: new  ServerRequestHandler(),
        responseHandler: new ServerResponseHandler()
    };
    
   return RestService.buildPostRequest(restRequest);
}

}
export class FormatTemplateGetAllClient {
public static GetAll (query: models.GetAllQuery) : Promise<models.GetAllResult> {
    const url = `api/FormatTemplates`;
    const restRequest:RestRequest = { 
        url:url,
        verb: 'GET',
        request:query,
        requestHandler: new  ServerRequestHandler(),
        responseHandler: new ServerResponseHandler()
    };
    
   return RestService.buildGetRequest(restRequest);
}

}
export class SortedDistinctListClient {
public static Clean (command: models.SortedDistinctListCommand) : Promise<models.SortedDistinctListResult> {
    const url = `api/FormatTemplates/SortedDistinctList`;
    const restRequest:RestRequest = { 
        url:url,
        verb: 'POST',
        request:command,
        requestHandler: new  ServerRequestHandler(),
        responseHandler: new ServerResponseHandler()
    };
    
   return RestService.buildPostRequest(restRequest);
}

}
export class WeatherForecastClient {
public static Get () : Promise<models.WeatherForecast[]> {
    const url = `WeatherForecast`;
    const restRequest:RestRequest = { 
        url:url,
        verb: 'GET',
        request:null,
        requestHandler: new  ServerRequestHandler(),
        responseHandler: new ServerResponseHandler()
    };
    
   return RestService.buildGetRequest(restRequest);
}

}   
