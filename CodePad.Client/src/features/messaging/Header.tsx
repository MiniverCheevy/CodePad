import { FunctionComponent, useEffect } from "react"
import { MessengerService } from "../../clients/messenger-service";

export const Header: FunctionComponent = () => {
    useEffect(() => {

    }, [MessengerService.numberOfPendingHttpRequest]);
    const show = MessengerService.numberOfPendingHttpRequest != 0;
    return (
        <div>
            {show && <div className="progress"> <div className="indeterminate teal darken-2"></div></div>}
        </div>
    );
}


