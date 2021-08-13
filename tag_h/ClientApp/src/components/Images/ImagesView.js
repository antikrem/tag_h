import React from 'react';

import { Controllers } from './../../Framework/Controllers'
import ViewModel from "react-use-controller";

import TaggedImageViewModel from './../Tags/TaggedImageViewModel'

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

class TiledTaggedImage {

    src = null;
    tagViewmodel = null;
    tagViewmodelProps = {};

    constructor(image) {
        this.src = `/Images/GetFile?imageId=${image.id}`;
        this.tagViewmodel = TaggedImageViewModel;
        this.tagViewmodelProps = { image: image }
    }
}

const ImagesView = () => {
    const { images } = ImagesViewModel.use();

    return (
        <div className='fill-parent'>
            <h1>Images view</h1>
            <p>Rendering images:</p>
            <ImagesTiling tiledImages={ images.map(image => new TiledTaggedImage(image)) } />
        </div>
    );
}

export default ImagesView;