import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Sidebar } from './Sidebar';
import { Content } from './Content' 

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className="layout">
                <Sidebar />
                <Content>
                    {this.props.children}
                </Content>
            </div>
        );
    }
}
