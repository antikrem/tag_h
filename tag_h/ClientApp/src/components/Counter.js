import React from 'react';
import ViewModel from "react-use-controller";

class CounterViewModel extends ViewModel {
    number = 1;

    increment = () => { this.number++ };
    decrement = () => { this.number-- };
}

const Counter = () => {
    const {  number, decrement, increment } = CounterViewModel.use();

    return (
        <div>
            <h1>Counter</h1>

            <p>This is a simple example of a React component.</p>

            <p aria-live="polite">Current count: <strong>{ number }</strong></p>

            <button className="btn btn-primary" onClick={increment}>Increment</button>
            <button className="btn btn-primary" onClick={decrement}>Decrement</button>
        </div>
    );
}

export default Counter;