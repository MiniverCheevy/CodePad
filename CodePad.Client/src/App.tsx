import './assets/sass/materialize.scss';
import './app.css';
import 'toastr/build/toastr.min.css';

import { useState } from 'react';
import './App.css';
import { Tabs } from './components/';
import * as React from 'react';
import { FormatTemplate } from "./features/templates/FormatTemplate";
import { Header } from './features/messaging/Header';

function App() {

    //const fetchWeatherForecast = () => {

    //    fetch('/WeatherForecast')
    //        .then((resp) => resp.json())
    //        .then((json) => {
    //            setResult(JSON.stringify(json));
    //            console.log(json);
    //        });
    //};

    //const fetchMessage = () => {
    //    fetch('/WeatherForecast/Message')
    //        .then((resp) => resp.text())
    //        .then((msg) => setResult(msg));
    //};
    return (

        <div className='App' >
            <Header></Header>

            <Tabs
                tabs={[
                    { title: "Format Template", active: true, content: <FormatTemplate /> },
                    { title: "Tab 2", active: true, content: <div></div> },
                    { title: "Tab 3", active: true, content: <div></div> },
                ]}
            />
        </div>
    );
}

export default App;
