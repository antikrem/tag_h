import React, { Component } from 'react';
import { SidebarItem } from './SidebarItem';
import './Sidebar.css';

export class Sidebar extends Component {

    constructor(props) {
        super(props);
        this.state = { open : true };
        this.toggleOpen = this.toggleOpen.bind(this);
    }

    toggleOpen() {
        this.setState({
            open: !this.state.open
        });
    }

    render() {
        return (<div className="sidebar">
            <button className="btn btn-primary" onClick={this.toggleOpen}>Close</button>
            <SidebarItem text="Home" link="/" />
            <SidebarItem text="Counter" link="/counter" />
            <SidebarItem text="Image View" link="/images-view" />
            <SidebarItem text="Import" link="/import-view" />
            <SidebarItem text="Logs" link="/log-view" />
        </div>);
    }
}