import React from 'react';
import { FileDrop } from 'react-file-drop';

import ImportViewModel from "./ImportViewModel"

const ImportView = (props) => {
    const { files, addFiles, submitFiles } = ImportViewModel.use();

    return (
        <div>
            {files.map(file => <p>{file.name}</p >)}
            <FileDrop
                onDrop={ (newFiles, _) => addFiles(newFiles) }
            >
                Drop some files here!
                </FileDrop>

            <button onClick={submitFiles }>Submit</button>
        </div>
    );
}

export default ImportView;