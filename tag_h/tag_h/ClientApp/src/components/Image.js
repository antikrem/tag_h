import React, { Component } from 'react';

//TODO
import { TagBox } from './Tags/TagBox'

export class Image extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.state = { image: props.image };
    }

    render() {
        return <div><img src={`/ImageProvider/?Get=${this.state.image.uuid}`} /> <TagBox image={this.state.image} /></div>;
    }
}
