import React, { Component } from 'react';

import { Controllers } from '../../Framework/Controllers'

class TagDropDownElement extends Component {
    constructor(props) {
        super(props);
        this.tag = props.tag;
        this.callback = () => props.callback(this.tag);
    }

    render() {
        return (
            <div>
                <button onClick={ this.callback }>{this.tag.name}</button>
            </div>
        );
    }
}

export class TagDropDown extends Component {

    constructor(props) {
        super(props);
        this.callback = props.callback;
        this.selectedTags = props.selectedTags;
        this.state = { tags: [] };
    }

    async componentDidMount() {
        this.setState({ tags: await Controllers.Tags.GetAllTags() })
    }

    render() {
        return (
            <div>
                {this.state.tags.filter(tag => !this.selectedTags.map(tag => tag.id).includes(tag.id)).map(
                    tag => <TagDropDownElement tag={tag} callback={ this.callback } />
                )}
            </div>
        );
    }
}