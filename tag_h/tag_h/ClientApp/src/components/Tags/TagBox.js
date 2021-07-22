import React, { Component } from 'react';
import { Tag } from './Tag'
import { TagAdd } from './TagAdd'

import { Controllers } from './../../Framework/Controllers'

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

    // TODO: use a viewcontroller
    async updateTags() {
        this.setState({ tags: await Controllers.ImageTags.GetTags(this.image.uuid) });
    }

    async deleteTag(tag) {
        await Controllers.ImageTags.DeleteTag(this.image.uuid, tag.value);
        this.setState({ tags: this.state.tags.filter(item => item.value !== tag.value) });
    }

    async addTag(tag) {
        await Controllers.ImageTags.AddTag(this.image.uuid, tag.value);
        this.updateTags();
    }

    render() {
        return (
            <div>
                {this.state.tags.map(tag =>
                    <Tag tag={tag} callback={ async () => await this.deleteTag(tag) } />
                )}
                <TagAdd callback={ async (tag) => await this.addTag(tag) }/>
            </div>
        );
    }
}
