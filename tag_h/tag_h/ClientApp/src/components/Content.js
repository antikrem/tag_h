import React, { Component } from 'react'
import { Container } from 'reactstrap';

export class Content extends Component {

    constructor(props) {
        super(props);
        this.children = props.children;
    }

    render() {
        return <div className="content">
            <Container>
                {this.props.children}
            </Container>
    </div>
    }
}