import React, { ReactNode } from 'react';
import M from 'materialize-css';
import { FunctionComponent, useEffect } from 'react';

export class AccordionItemConfig {
    text?: string;
    show: boolean = true;
    id?: string;
    children?: ReactNode;
}

export const AccordionItem: FunctionComponent<AccordionItemConfig> = (props: AccordionItemConfig) => {

    return (

            <li>
                <div className="collapsible-header"><i className="material-icons">filter_drama</i>First</div>
                <div className="collapsible-body">{props.children}</div>
            </li>
            <li>
              <div className="collapsible-header"><i className="material-icons">place</i>Second</div>
              <div className="collapsible-body"><span>Lorem ipsum dolor sit amet.</span></div>
            </li>
            <li>
              <div className="collapsible-header"><i className="material-icons">whatshot</i>Third</div>
              <div className="collapsible-body"><span>Lorem ipsum dolor sit amet.</span></div>
            </li>
        </ul>


        //<Accordion defaultExpanded={props.show}>
        //    <AccordionSummary
        //        expandIcon={<ExpandMoreIcon />}
        //        aria-controls="panel1-content"
        //        id="panel1-header">
        //        <Typography>{props.text}</Typography>
        //    </AccordionSummary>
        //    <AccordionDetails>
        //        {props.children}
        //    </AccordionDetails>
        //</Accordion>
    );
};
