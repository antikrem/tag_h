import * as React from 'react'

import './Image.css'

export interface ImageTileModel {
    src: null | string;
    id: number;
}

export interface ImageTileProps {
    tiledImage: ImageTileModel;
    setImage?: (model: ImageTileModel) => void;
}

export const ImageTile = (props: ImageTileProps) => {

    var setImage = props.setImage ?? (() => { });

    return <div className='inline-image-container' onClick={() => setImage(props.tiledImage)}>
        <img className='inline-image' src={props.tiledImage.src!} />
    </div>
}
