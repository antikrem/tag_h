import React from 'react';

import Image from './Image'

import './Image.css'

const ImagesTiling = (props) => {

  return (
    <div className='image-grid fill-parent'>
      {props.images.map((image, i) => <Image key={i} image={image} />)}
    </div>
  );
}

export default ImagesTiling;