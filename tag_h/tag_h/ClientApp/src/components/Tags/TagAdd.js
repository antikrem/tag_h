import React, { Component } from 'react';
import { TagDropDown } from './TagDropDown'

export class TagAdd extends Component {

    constructor(props) {
        super(props);
        this.callback = props.callback;
        this.state = { active: false };
    }

    componentDidMount() {
    }

    componentWillUnmount() {
        this.deactivateDropDown();
    }

    activateDropDown() {
        document.addEventListener("click", this.handleClickOutside);
        this.setState({ active: true });
    }

    deactivateDropDown() {
        document.removeEventListener("click", this.handleClickOutside);
        this.setState({ active: false });
    }

    handleClickOutside = event => {
        const path = event.path || (event.composedPath && event.composedPath());
 
        if (!path.includes(this.ref)) {
            this.deactivateDropDown();
        }
    };

    render() {
        return (
            <div>
                <button onClick={ () => this.activateDropDown() }> |+| </button>
                {this.state.active && <TagDropDown ref={ref => (this.ref = ref)} callback={  this.callback }/>}
            </div>
        );
    }
}