import React from 'react';

import { Controllers } from './../../Framework/Controllers'
import ViewModel from "react-use-controller";

import TaggedImageViewModel from './../Tags/TaggedImageViewModel'
import TagBox from './../Tags/TagBox'

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

    constructor(image) {
        this.id = image.id;
        this.src = `/Images/GetFile?imageId=${image.id}`;
    }
}

const ImagesView = () => {
    const { images } = ImagesViewModel.use();
    const { tags, image, deleteTag, addTag, setImage } = TaggedImageViewModel.use()

    return (
        <div className='fill-parent'>
            <h1>Images view</h1>
            <p>Rendering images:</p>
            <ImagesTiling tiledImages={images.map(image => new TiledTaggedImage(image))} setImage={setImage} />
            {image && <TagBox viewmodel={{ tags, image, deleteTag, addTag }} />}
        </div>
    );
}

export default ImagesView;