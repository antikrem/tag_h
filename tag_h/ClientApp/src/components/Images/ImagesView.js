import React from 'react';

import { Controllers } from './../../Framework/Controllers'
import ViewModel from "react-use-controller";

import ImagesTiling from './ImagesTiling'

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
        <div className='fill-parent'>
            <h1>Images view</h1>
            <p>Rendering images:</p>
            <ImagesTiling images={ images } />
        </div>
    );
}

export default ImagesView;