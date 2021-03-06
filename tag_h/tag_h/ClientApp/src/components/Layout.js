import React, { Component } from 'react';
import { Sidebar } from './Sidebar';
import { Content } from './Content';

import "./Layout.css"

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className="layout">
                <Sidebar />
                <Content>
                    { this.props.children }
                </Content>
            </div>
        );
    }
}
