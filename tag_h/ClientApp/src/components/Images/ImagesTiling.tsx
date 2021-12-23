import * as React from 'react';

import { ImageTile, ImageTileModel } from './ImageTile';

import './Image.css'
import './../../Styles/Format.css'

export interface ImageTileViewProps<T extends ImageTileModel> {
    tiledImages: T[];
    setImage?: (model: T) => void;
}

export const ImagesTiling = <T extends ImageTileModel> (props: ImageTileViewProps<T>) => {
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