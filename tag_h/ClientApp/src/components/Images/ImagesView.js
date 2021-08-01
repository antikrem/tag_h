import React from 'react';

import { Controllers } from './../../Framework/Controllers'
import ViewModel from "react-use-controller";

import { Image } from './Image'

import './Image.css'

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
        <div className='image-grid fill-parent'>

            {images.map((image, i) => <Image key={i} image={image} />)}
        </div>
    );
}

export default ImagesView;