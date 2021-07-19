import React, { Component } from 'react';

import { Controllers } from '../../Framework/Controllers'

export class TagDropDown extends Component {

    constructor(props) {
        super(props);
        this.state = { tags: [] };
    }

    async componentDidMount() {
        this.setState({ tags: await Controllers.Tags.GetAllTags() })
    }

    render() {
        return (
            <div>
                {this.state.tags != null && this.state.tags.map(
                    tag => <div><p>{tag.value}</p></div>
                )}
            </div>
        );
    }
}