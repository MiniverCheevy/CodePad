import React, { FunctionComponent, useEffect } from 'react';
import M from 'materialize-css';
import { GetId } from './../id-factory';
class CollapsibleConfig {
    children: React.ReactNode;
}

const Collapsible: FunctionComponent<CollapsibleConfig> = (props: CollapsibleConfig) => {
    useEffect(() => {
        M.Collapsible.init(document.querySelectorAll('.collapsible'));
    }, []);
    const id = GetId('collapsible');
    return (
        <ul className="collapsible" id={id}>
            {props.children}
        </ul>
    );
};

export default Collapsible;