import { reactive } from 'event-reduce-react';
import * as React from 'react';
import { PageManagementEvents } from "../PageSystem/PageManagement";
import { PageModel } from '../PageSystem/PageModel';
import './Sidebar.css';

interface ISidebarItemProps {
    page: PageModel,
    pageEvent: PageManagementEvents
}

export const SidebarItem = reactive(function App(props: ISidebarItemProps) {
    return (
        <div className="sidebar-item">
            <button onClick={() => props.pageEvent.selectPage(props.page)}>
                <p>{props.page.name}</p>
            </button>
        </div>
    );
});