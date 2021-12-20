import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { App } from './App';
import { ApplicationModel } from './ApplicationModel';
import bindApiControllers from './Framework/ApiControllerBinder';
import registerServiceWorker from './Framework/registerServiceWorker';

bindApiControllers().then(
    _ => {
        const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
        const rootElement = document.getElementById('root');

        let model = new ApplicationModel();

        ReactDOM.render(
            <BrowserRouter basename={baseUrl}>
                <App model={model} />
            </BrowserRouter>,
            rootElement);

        registerServiceWorker();
    }
)