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
        const response = await fetch(`/ImageTagProvider/GetTags?uuid=${this.image.uuid}`);
        const tags = await response.json();
        this.setState({ tags: tags });
    }

    async DeleteTag(tag) {
        await fetch(`/ImageTagProvider/DeleteTag?uuid=${this.image.uuid}&tagName=${tag.value}`, { method: 'DELETE' })
        this.setState({ tags: this.state.tags.filter(item => item.value !== tag.value) })
    }

    render() {
        return (
            <div>
                {this.state.tags.map(tag =>
                    <Tag tag={tag} callback={async () => await this.DeleteTag(tag)} />
                )}
            </div>
        );
    }
}
