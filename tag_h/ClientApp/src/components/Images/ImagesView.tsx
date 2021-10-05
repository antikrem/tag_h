import * as React from 'react';

import { Controllers } from '../../Framework/Controllers';
import ViewModel from 'react-use-controller';

import TaggedImageViewModel from '../Tags/TaggedImageViewModel';
import TagBox from '../Tags/TagBox';

import { ImageTileModel } from './ImageTile';
import ImagesTiling from './ImagesTiling';

import './Image.css'
import './../../Styles/Format.css'

class ImagesViewModel extends ViewModel {
    
    images = [];

    componentDidMount() {
        this.fetchImages();
    }

    async fetchImages() {
        this.images = await Controllers.Images.GetAll();
    }
}

class TiledTaggedImage implements ImageTileModel {
    src: string;
    id: number;

    constructor(image) {
        this.id = image.id;
        this.src = `/Images/GetFile?imageId=${image.id}`;
    }
}

const ImagesView = () => {
    const { images } = ImagesViewModel.use();
    const { tags, image, deleteTag, addTag, setImage } = TaggedImageViewModel.use()

    //TODO: Refactor to make task-pane more generic
    return (
        <div className='fill-parent'>
            <div className={'image-body center' + (image ? ' left' : '')}>
                <ImagesTiling
                    tiledImages={images.map(image => new TiledTaggedImage(image))}
                    setImage={setImage} />
            </div>
            {image && <div className='image-task-pane scrollbar'>
                <TagBox viewmodel={{ tags, image, deleteTag, addTag }}/>
            </div>}
        </div>
    );
}

export default ImagesView;