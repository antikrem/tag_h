import React from 'react';

import { SidebarItem } from './PanelSystem/SidebarItem';

import PanelViewModel from './PanelSystem/PanelsViewModel';
import SideBarViewModel from './PanelSystem/SidebarViewModel'

import './custom.css'
import './components/Content.css'
import './PanelSystem/Sidebar.css';
import './components/Layout.css';


const App = () => {
    const { activePane, getSidebarProps } = PanelViewModel.use();
    const { collapsed, toggleCollapsed } = SideBarViewModel.use();

    return (
        <div className="layout">
            {!collapsed && <div className="sidebar">
                <button className="btn btn-primary" onClick={() => { toggleCollapsed() }}>Close</button>
                {getSidebarProps().map((props, i) => <SidebarItem key={i} {...props} />)}
            </div>}
            <div className="content">
                {activePane}
            </div>
        </div>
    );
}

export default App;