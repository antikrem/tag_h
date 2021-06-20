import React, { Component } from 'react'
import './Sidebar.css';

export class Sidebar extends Component {
    render() {
        return (<div className="sidebar-container">
            <div className="sidebar">
                <div className="sidebar-link">Home</div>
                <div className="sidebar-link">About</div>
                <div className="sidebar-link">Contact</div>
            </div>
        </div>);
    }
}