import React, { Component } from 'react';

export class Tag extends Component {

    constructor(props) {
        super(props);
        this.callback = props.callback;
        this.state = { tag: props.tag };
    }

    render() {
        return (
            <div>
                <p> {this.state.tag.value} </p><button onClick={this.callback}> |x| </button>
            </div>
        );
    }
}