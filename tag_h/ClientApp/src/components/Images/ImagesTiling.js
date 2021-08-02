import React from 'react';

import { ImageTile } from './ImageTile'

import './Image.css'

const ImagesTiling = (props) => {
    return (
        <div className='image-grid fill-parent'>
            {props.images.map((image, i) => <ImageTile key={i} src={`/Images/GetFile?imageId=${image.uuid}`} />)}
            {/*<TagBox image={this.state.image} viewmodel={TaggedImageViewModel} />*/}
        </div>
    );
}

export default ImagesTiling;