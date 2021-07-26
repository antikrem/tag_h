import React from 'react';
import { FileDrop } from 'react-file-drop';

import "./ImportViewModel.js"
const ImportView = (props) => {
    const { files, addFiles } = ImportViewModel.use();

    return (
        <div>
            {files.map(file => <p>{file.name}</p >)}
            <FileDrop
                onDrop={ (files, _) => {files = files.concat(Array.from(files))} }
            >
                Drop some files here!
                </FileDrop>

            <button onClick={ addFiles }>Submit</button>
        </div>
    );
}

export default ImportView;