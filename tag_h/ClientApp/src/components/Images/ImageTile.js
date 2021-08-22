import React from 'react'

import './Image.css'

const ImageTile = (props) => {

    var setImage = props.setImage || (() => { });

    return <div className='inline-image-container' onClick={() => setImage(props.tiledImage)}>
        <img className='inline-image' src={props.tiledImage.src} />
    </div>
}

export default ImageTile;