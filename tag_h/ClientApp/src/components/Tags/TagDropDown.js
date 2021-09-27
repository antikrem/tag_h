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
        this.state = { tags: [], search: "" };
    }

    async componentDidMount() {
        // TODO: use controller to do search
        this.setState({ tags: await Controllers.Tags.GetAllTags(), search: this.state.search })

        document.addEventListener('keydown', this.onKey);
    }

    async componentWillUnmount() {
        document.removeEventListener('keydown', this.onKey);
    }

    onKey = (event) => {
        const key = event.key.toLowerCase();
        if ((/[a-zA-Z]/).test(key)) {
            this.setState({ tags: this.state.tags, search: this.state.search + key });
        }
    }

    render() {

        var tags = this.state.tags;
        tags = tags.filter(tag => !this.selectedTags.map(tag => tag.id).includes(tag.id));
        if (this.state.search != "") { // TODO: make this iterate over once
            tags = tags.filter(tag => tag.name.indexOf(this.state.search) >= 0);
        }

        return (
            <div>
                {tags.map(
                    tag => <TagDropDownElement key={tag.id} tag={tag} callback={ this.callback } />
                )}
            </div>
        );
    }
}