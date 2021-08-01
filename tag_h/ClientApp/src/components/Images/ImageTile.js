import React, { Component } from 'react'

export class ImageTile extends Component {

    constructor(props) {
        super(props);
        this.state = { src: props.src };
    }

    render() {
        return <img style={{ width: "100%" }} src={this.state.src} />;
    }
}