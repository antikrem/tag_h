import React from 'react';
import { FileDrop } from 'react-file-drop';

import ImagesTiling from './../Images/ImagesTiling'
import TagBox from './../Tags/TagBox';

import ImportViewModel from './ImportViewModel';
import ImportTagViewModel from './ImportTagViewModel';

class TiledFileImage {

    constructor(file) {
        this.src = URL.createObjectURL(file);
    }
}

const ImportView = (props) => {
    const { files, addFiles, setTags, submitFiles } = ImportViewModel.use();

    return (
        <div>
            <FileDrop onDrop={ (newFiles, _) => addFiles(newFiles) } >
                Drop some files here!
            </FileDrop>
            <button onClick={submitFiles}>Submit</button>
            {<TagBox viewmodel={ImportTagViewModel} setTags={setTags} />}
            <ImagesTiling tiledImages={files.map(file => new TiledFileImage(file))} />
        </div>
    );
}

export default ImportView;