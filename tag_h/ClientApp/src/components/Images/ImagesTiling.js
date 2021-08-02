import React from 'react';

import ImageTile from './ImageTile'

import './Image.css'

const ImagesTiling = (props) => {
    return (
        <div className='image-grid fill-parent'>
            {props.tiledImages.map((tiledImage, i) =>
                <ImageTile
                    key={i}
                    tiledImage={tiledImage}
                    viewmodel={props.viewmodel}/>)
            }
        </div>
    );
}

export default ImagesTiling;