import React from 'react'

import TagBox from './../Tags/TagBox'

import './Image.css'

const ImageTile = (props) => {

    return <div className='inline-image-container'>
        <img className='inline-image' src={props.tiledImage.src} />

        {props.tiledImage.tagViewmodel != null
            && <TagBox viewmodel={props.tiledImage.tagViewmodel} {...props.tiledImage.tagViewmodelProps} />}
    </div>
}


export default ImageTile;