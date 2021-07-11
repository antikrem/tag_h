import React, { Component } from 'react';
import { Image } from './Image'

import { Controllers } from './../Framework/Controllers'

export class ImagesView extends Component {
    static displayName = ImagesView.name;

    constructor(props) {
        super(props);
        this.state = { images: [], loading: true };
    }

    componentDidMount() {
        this.fetchImages();
    }

    static CreateImageView(images) {
        return (
            images.map(image =>
                <Image image={image}/>
            )
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ImagesView.CreateImageView(this.state.images);

        return (
            <div>
                <h1 id="tabelLabel" >Images view</h1>
                <p>Rendering images:</p>
                {contents}
            </div>
        );
    }

    async fetchImages() {
        const images = await Controllers.Images.GetAll();
        this.setState({ images: images, loading: false });
    }
}
