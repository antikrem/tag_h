import React from 'react';
import { FileDrop } from 'react-file-drop';

import TagBox from './../Tags/TagBox';

import ImportViewModel from './ImportViewModel';
import ImportTagViewModel from './ImportTagViewModel';

const ImportView = (props) => {
    const { files, addFiles, setTags, submitFiles } = ImportViewModel.use();

    return (
        <div>
            {files.map(file => <p>{file.name}</p >)}
            <FileDrop onDrop={ (newFiles, _) => addFiles(newFiles) } >
                Drop some files here!
            </FileDrop>
            <button onClick={submitFiles}>Submit</button>
            <TagBox viewmodel={ImportTagViewModel} setTags={setTags} />
        </div>
    );
}

export default ImportView;