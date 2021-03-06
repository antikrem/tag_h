﻿import React, { Component } from 'react';
import { LogEntry } from './LogEntry'

import { Controllers } from './../Framework/Controllers'

export class LogView extends Component {
    static displayName = LogView.name;

    constructor(props) {
        super(props);
        this.state = { logs: [], loading: true };
    }

    componentDidMount() {
        this.fetchLogs();
    }

    static CreateLogsView(logs) {
        console.log(logs)
        return (
            <table>
                <tr>
                    <th>TimeStamp</th>
                    <th>Message</th>
                </tr>
                {logs.map(log =>
                    <LogEntry logEntry={log} />
                )}
            </table>
            
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : LogView.CreateLogsView(this.state.logs);

        return (
            <div>
                <h1>Log view</h1>
                <p>Rendering images:</p>
                {contents}
            </div>
        );
    }

    async fetchLogs() {
        const logs = await Controllers.Logging.Get();
        this.setState({ logs: logs, loading: false });
    }
}
