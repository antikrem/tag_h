import React, { Component } from 'react';

import { Controllers } from './../Framework/Controllers'

export class Tag extends Component {

    constructor(props) {
        super(props);
        this.state = { tags: [] };
    }

    componentDidMount() {
        this.state.tags = Controllers.Tags.GetAllTags();
    }

    render() {
        return (
            <div>
                <button onClick={() => this.callback()}> |+| </button>
            </div>
        );
    }
}