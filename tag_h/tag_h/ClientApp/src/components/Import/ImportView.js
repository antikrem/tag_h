import React, { Component } from 'react';
import { FileDrop } from 'react-file-drop';

import { Controllers } from './../../Framework/Controllers'

export class ImportView extends Component {
    static displayName = ImportView.name;

    constructor(props) {
        super(props);
        this.state = { files: [] };
    }

    static toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result.replace("data:", "").replace(/^.+,/, ""));
        reader.onerror = error => reject(error);
    });

    async addFiles() {
        this.state.files.forEach(
            async file => {
                await Controllers.Images.AddImages([{ data: await ImportView.toBase64(file), fileName: file.name }])
            }
        );
        this.setState({ files: [] })
    }

    render() {
        return (
            <div>
                {this.state.files.map(file => <p>{file.name}</p >)}
                <FileDrop
                    onDrop={(files, _) => this.setState({ files: this.state.files.concat(Array.from(files)) })}
                >
                    Drop some files here!
                </FileDrop>

                <button onClick={ () => this.addFiles() }>Submit</button>
            </div>
        );
    }
}
