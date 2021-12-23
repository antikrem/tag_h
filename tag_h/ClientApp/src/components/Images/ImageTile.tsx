import * as React from 'react'

import './Image.css'

export interface ImageTileModel {
    src: null | string;
    id: number;
}

export interface ImageTileProps<T extends ImageTileModel> {
    tiledImage: T;
    setImage?: (model: T) => void;
}

export const ImageTile = <T extends ImageTileModel>(props: ImageTileProps<T>) => {

    var setImage = props.setImage ?? (() => { });

    return <div className='inline-image-container' onClick={() => setImage(props.tiledImage)}>
        <img className='inline-image' src={props.tiledImage.src!} />
    </div>
}
