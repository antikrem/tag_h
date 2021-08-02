import React, { Component } from 'react'

import './Image.css'

export class ImageTile extends Component {

    constructor(props) {
        super(props);
        this.state = { src: props.src };
    }

    render() {
        return <img className='inline-image' src={this.state.src} />;
    }
}
