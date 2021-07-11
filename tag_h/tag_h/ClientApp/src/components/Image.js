import React, { Component } from 'react';
import { TagBox } from './Tags/TagBox'


import { Controllers } from './../Framework/Controllers'

export class Image extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.state = { image: props.image };
    }

    render() {
        return <div><img src={`/Images/GetFile?imageId=${this.state.image.uuid}`} /> <TagBox image={this.state.image} /></div>;
    }
}
