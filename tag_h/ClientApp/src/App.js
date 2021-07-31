import React, { Component } from 'react';

import { Content } from './components/Content';
import { SidebarItem } from './components/SidebarItem';

import PanelViewModel from './PanelSystem/PanelsViewModel';

import './custom.css'
import './components/Sidebar.css';
import './components/Layout.css';


const App = () => {
    const { activePane, getSidebarProps } = PanelViewModel.use();

    return (
        <div className="layout">
            <div className="sidebar">
                <button className="btn btn-primary" onClick={() => { }}>Close</button>
                {getSidebarProps().map((props, i) => <SidebarItem key={i} {...props} />)}
            </div>
            <Content>
                {activePane}
            </Content>
        </div>
    );
}

export default App;