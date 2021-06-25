import React, { Component } from 'react';
import { SidebarItem } from './SidebarItem';
import './Sidebar.css';

export class Sidebar extends Component {
    render() {
        return (<div className="sidebar">
            <SidebarItem text="Home" link="/" />
            <SidebarItem text="Counter" link="/counter" />
            <SidebarItem text="Image View" link="/images-view" />
            <SidebarItem text="Fetch Data" link="/fetch-data" />
        </div>);
    }
}