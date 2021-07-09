import React, { Component } from 'react';
import { FileDrop } from 'react-file-drop';

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
        this.state.files.forEach(async file => {
            console.log(file);
            await fetch(
                `/Images/AddImages`,
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify([{ data: await ImportView.toBase64(file), fileName: file.name }])
                }
            );
        }
        );

        this.setState({ files: [] })
    }

    render() {
        return (
            <div>
                <FileDrop
                    onDrop={(files, _) => this.setState({ files: this.state.files.concat(Array.from(files)) })}
                >
                    Drop some files here!
                </FileDrop>

                <button onClick={ () => this.addFiles() }/>
            </div>
        );
    }
}
