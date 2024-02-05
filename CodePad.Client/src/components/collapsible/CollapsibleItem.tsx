import React, { FunctionComponent, useEffect } from 'react';
import M from 'materialize-css';

class CollapsibleItemConfig {
    header: string = '';
    children: React.ReactNode;
    open: boolean = false;
}

const CollapsibleItem: FunctionComponent<CollapsibleItemConfig> = (props: CollapsibleItemConfig) => {
    return (
        <li className={props.open ? 'active' : ''}>
            <div className="collapsible-header">{props.header}</div>
            <div className="collapsible-body">{props.children}</div>
        </li>
    );
};

export default CollapsibleItem;