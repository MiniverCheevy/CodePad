// This class is an extensibility point, use it to integrate with your state management system of choice
import M from 'materialize-css';
import toastr from 'toastr';
export class MessengerService {
    public static numberOfPendingHttpRequest = 0;

    public static showToast = (text: string, isError: boolean) => {
        if (text == null || text == undefined || text == '')
        { return; }
        if (!isError) {
            toastr.error(text);            
        }
        else {
            toastr.success(text);
        }
    }

    public static incrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest += 1;
    }
    public static decrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest -= 1;
    }
}