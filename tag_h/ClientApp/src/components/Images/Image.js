import React, { Component } from 'react';

import ImageTile from './ImageTile'
import TagBox from './../Tags/TagBox'

import TaggedImageViewModel from './../Tags/TaggedImageViewModel'

import './Image.css'

export default class Image extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.state = { image: props.image };
    }

    render() {
        return <div className='inline-image'>
            <ImageTile src={`/Images/GetFile?imageId=${this.state.image.uuid}`} />
            {/*<TagBox image={this.state.image} viewmodel={TaggedImageViewModel} />*/}
        </div>;
    }
}