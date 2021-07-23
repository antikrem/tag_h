import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import ImagesView from './components/ImagesView';
import { LogView } from './components/LogView';
import Counter from './components/Counter';
import { Image } from './components/Image';
import { ImportView } from './components/ImportView'

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/images-view' component={ImagesView} />
                <Route path='/log-view' component={LogView} />
                <Route path='/image' component={Image} />
                <Route path='/import-view' component={ImportView} />
            </Layout>
        );
    }
}
