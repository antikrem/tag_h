import React, { Component } from 'react';

export class Tag extends Component {

    constructor(props) {
        super(props);
        this.callback = props.callback;
        this.state = { tag: props.tag };
    }

    render() {
        return (
            <span style={{ marginRight: '5px', padding: '5px', whiteSpace: 'nowrap',  backgroundColor: 'red' }}>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block' }}> {this.state.tag.name}</p>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block', cursor: 'pointer' }} onClick={() => this.callback()}> x </p>
            </span>
        );
    }
}