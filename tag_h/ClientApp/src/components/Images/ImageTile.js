import React from 'react'

import TagBox from './../Tags/TagBox'

import './Image.css'

const ImageTile = (props) => {

    return <div className='inline-image-container'>
        <img className='inline-image' src={props.tiledImage.src} />
    </div>
}


export default ImageTile;