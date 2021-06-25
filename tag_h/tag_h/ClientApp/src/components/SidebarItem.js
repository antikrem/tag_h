import React, { Component } from 'react'
import './Sidebar.css';

export class SidebarItem extends Component {
    constructor(props) {
        super(props);

        this.text = props.text;
        this.link = props.link;
    }

    render() {
        return (
            <a href={this.link}>
                <div className="sidebar-item">
                    <p>{this.text}</p>
                </div>
            </a>);
    }
}