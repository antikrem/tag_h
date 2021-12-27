import { asyncEvent, reduce, reduced } from "event-reduce";
import { reactive } from "event-reduce-react";
import * as React from 'react';
import { useEffect } from 'react';
import { Controllers } from './../Framework/Controllers';
import { PageModel } from "./../PageSystem/PageModel";
import { LogEntry } from './../Typings/LogEntry';
import { LogsRow } from './LogsRow';

export class LogEvents {
    fetchLogs = asyncEvent<LogEntry[]>();
}

export class LogsModel extends PageModel {
    name = "Logs";

    events = new LogEvents();

    @reduced
    logs = reduce([] as LogEntry[], this.events)
        .on(e => e.fetchLogs.resolved, (_, { result: logs }) => logs)
        .value;

    view() {
        return <Logs model={this} />;
    }
}

export const Logs = reactive(function Logs({ model }: { model: LogsModel }) {
    useEffect(() => model.events.fetchLogs(Controllers.Logging.Get()), [model])

    return (
        <div>
            <h1>Log view</h1>
            <table>
                <thead>
                    <tr>
                        <th>TimeStamp</th>
                        <th>Message</th>
                    </tr>
                </thead>
                <tbody>
                    {model.logs.map(log =>
                        <LogsRow key={log.time} log={log} />
                    )}
                </tbody>
            </table>
        </div>
    );
});