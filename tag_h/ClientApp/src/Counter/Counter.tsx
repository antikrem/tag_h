import * as React from 'react';

import { event, reduce, reduced } from "event-reduce";
import { reactive } from "event-reduce-react";
import { PageModel } from '../PageSystem/PageModel';

export class CounterModel extends PageModel
{
    name = "Counter";

    increment = event();
    decrement = event();

    @reduced
    count = reduce(0)
        .on(this.increment, (current) => current + 1)
        .on(this.decrement, (current) => current - 1)
        .value;

    constructor() {
        super();
    }

    view() { 
        return <Counter model={this}/> 
    }
}

export const Counter = reactive(function Counter({ model }: { model: CounterModel }) {
    return (
        <div>
            <h1>Counter</h1>

            <p>This is a simple example of a React component.</p>

            <p aria-live="polite">Current count: <strong>{model.count}</strong></p>

            <button className="btn btn-primary" onClick={() => model.increment()}>Increment</button>
            <button className="btn btn-primary" onClick={() => model.decrement()}>Decrement</button>
        </div>
    );
});

