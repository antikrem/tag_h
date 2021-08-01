import React from 'react';

import { SidebarItem } from './PanelSystem/SidebarItem';

import PanelViewModel from './PanelSystem/PanelsViewModel';

import './custom.css'
import './components/Content.css'
import './PanelSystem/Sidebar.css';
import './components/Layout.css';


const App = () => {
    const { activePane, getSidebarProps } = PanelViewModel.use();

    return (
        <div className="layout">
            <div className="sidebar">
                <button className="btn btn-primary" onClick={() => { }}>Close</button>
                {getSidebarProps().map((props, i) => <SidebarItem key={i} {...props} />)}
            </div>
            <div className="content">
                {activePane}
            </div>
        </div>
    );
}

export default App;