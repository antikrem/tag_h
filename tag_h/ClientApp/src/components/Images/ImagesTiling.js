import React from 'react';

import ImageTile from './ImageTile'

import './Image.css'
import './../../Styles/Format.css'

const ImagesTiling = (props) => {
    return (
        <div className='scrollbar'>
            <div className='image-grid fill-parent'>
                {props.tiledImages.map((tiledImage, i) =>
                    <ImageTile
                        key={i}
                        tiledImage={tiledImage}
                        setImage={props.setImage}/>)
                }
            </div>
        </div >
    );
}

export default ImagesTiling;