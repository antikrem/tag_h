import React, { Component } from 'react';

export class Image extends Component {
    static displayName = Image.name;

    constructor(props) {
        super(props);
        this.state = { image: props.image };
    }

    render() {
        return <img src={`/ImageProvider/?Get=${this.state.image.uuid}`} />;
    }
}
