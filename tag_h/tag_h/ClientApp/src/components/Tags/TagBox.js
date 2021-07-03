import React, { Component } from 'react';
import { Tag } from './Tag'

export class TagBox extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.image = props.image;
        this.state = { tags: [] };
    }

    async componentDidMount() {
        await this.updateTags();
    }

    async updateTags() {
        const response = await fetch(`/ImageTagProvider?uuid=${this.image.uuid}`);
        const tags = await response.json();
        this.setState({ tags: tags });
    }

    render() {
        return (
            <div>
                {this.state.tags.map(tag =>
                    <Tag tag={tag} />
                )}
            </div>
        );
    }
}
