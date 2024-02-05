import React, { FunctionComponent, useEffect } from 'react';
import M from 'materialize-css';
import { GetId } from './../id-factory';

class TabsConfig {
    tabs: TabConfig[] = [];
}
class TabConfig {
    title: string = '';
    active?: boolean = false;
    content: React.ReactNode;
    key?: string;
}

const Tabs: FunctionComponent<TabsConfig> = (props: TabsConfig) => {

    useEffect(() => {
        M.Tabs.init(document.querySelectorAll('.tabs'));
    }, []);

    const headers = props.tabs.map(
        (tab, index) => {
            const id = GetId('tabs-' + index.toString() + '-');
            tab.key = id;
            return (
                <li className={`tab ${tab.active ? 'active' : ''}`} key={`link-${id}` } >
                    <a href={`#${id}`}>{tab.title}</a>
                </li>);
        });
    

    const contents = props.tabs.map(
        (tab, index) => {
            return (
                <div id={tab.key} className="col s12" key={`content-${tab.key}`}>{tab.content}</div>
            );
        });    

    return (
        <div className="row">
            <div className="col s12">
                <ul className="tabs">
                    {headers}
                </ul>
            </div>
            {contents}
        </div>
    );
};

export default Tabs;