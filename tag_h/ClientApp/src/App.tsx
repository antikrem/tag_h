import { reactive } from 'event-reduce-react';
import * as React from 'react';
import { ApplicationModel } from './ApplicationModel';
import './components/Content.css';
import './components/Layout.css';
import './custom.css';
import './PageSystem/Sidebar.css';
import { SidebarItem } from './PageSystem/SidebarItem';

export const App = reactive(function App({ model }: { model : ApplicationModel }) {
    return (
        <div className="layout">
            {!model.sidePanel.collapsed && <div className="sidebar">
                <button className="btn btn-primary" onClick={() => model.sidePanel.toggleCollapse() }>Close</button>
                {model.pages.map((page, i) => <SidebarItem key={i} page={page} pageEvent={model.pageManagement.events} />)}
            </div>}
            <div className="content">
                { model.pageManagement.activePage.view() }
            </div>
        </div>
    );
});