import React, { Component } from 'react'
import './Sidebar.css';

export class SidebarItem extends Component {
    constructor(props) {
        super(props);

        this.text = props.text;
        this.onClick = props.onClick;
    }

    render() {
        return (
            <div className="sidebar-item">
                <button onClick={this.onClick}><p>{this.text}</p></button>
            </div>
        );
    }
}