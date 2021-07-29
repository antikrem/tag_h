import React, { Component } from 'react';
import { Home } from './components/Home';
import ImagesView from './components/ImagesView';
import { LogView } from './components/LogView';
import Counter from './components/Counter';
import ImportView from './components/Import/ImportView'
import { Content } from './components/Content';
import { SidebarItem } from './components/SidebarItem';

import './custom.css'
import './components/Sidebar.css';
import './components/Layout.css';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div className="layout">
                <div className="sidebar">
                    <button className="btn btn-primary" onClick={this.toggleOpen}>Close</button>
                    <SidebarItem text="Home" link="/" />
                    <SidebarItem text="Counter" link="/counter" />
                    <SidebarItem text="Image View" link="/images-view" />
                    <SidebarItem text="Import" link="/import-view" />
                    <SidebarItem text="Logs" link="/log-view" />
                </div>
                <Content>
                    <Home />
                    <Counter />
                    <ImagesView />
                    <LogView />
                    <ImportView />
                </Content>
            </div>
        );
    }
}
