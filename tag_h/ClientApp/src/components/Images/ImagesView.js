import React from 'react';

import { Controllers } from './../Framework/Controllers'
import ViewModel from "react-use-controller";

import { Image } from './Image'

class ImagesViewModel extends ViewModel {
    
    images = [];

    componentDidMount() {
        this.fetchImages();
    }

    async fetchImages() {
        this.images = await Controllers.Images.GetAll();
    }
}

const ImagesView = () => {
    const { images } = ImagesViewModel.use();

    return (
        <div>
            <h1 id="tabelLabel" >Images view</h1>
            <p>Rendering images:</p>
            {images.map(image => <Image image={image} />)}
        </div>
    );
}

export default ImagesView;