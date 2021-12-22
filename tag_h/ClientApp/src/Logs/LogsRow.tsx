import React from 'react';
import { LogEntry } from '../Typings/LogEntry'

export function LogsRow({ log }: { log: LogEntry }) {
    return <tr>
        <td>{log.time}</td><td> {log.body}</td>
    </tr>;
};