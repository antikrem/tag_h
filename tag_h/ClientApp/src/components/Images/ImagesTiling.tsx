import * as React from 'react';

import { ImageTile, ImageTileModel } from './ImageTile';

import './Image.css'
import './../../Styles/Format.css'

export interface ImageTileViewProps {
    tiledImages: ImageTileModel[];
    setImage?: (model: ImageTileModel) => void;
}

const ImagesTiling = (props: ImageTileViewProps) => {
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