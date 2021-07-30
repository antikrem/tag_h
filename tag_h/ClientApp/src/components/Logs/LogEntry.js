import React, { Component } from 'react';

export class LogEntry extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.state = { logEntry: props.logEntry };
    }

    render() {
        return <tr><td> {this.state.logEntry.postTime}</td><td> {this.state.logEntry.body}</td></tr>;
    }
}
