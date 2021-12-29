import { reactive } from 'event-reduce-react';
import React from 'react';
import { FileDrop } from 'react-file-drop';
import { Controllers } from '../Framework/Controllers';
import { ImportModel } from './Import';

export const ImportPane = reactive(function Images({ model }: { model: ImportModel }) {
    return (
        <div>
            <FileDrop onDrop={(newFiles, _) => newFiles && model.events.addFiles(Array.from(newFiles))}>
                Drop some files here!
            </FileDrop>
            <button onClick={() =>  model.events.submitImages(submit(model))}>
                Submit
            </button>
        </div>
    );
});

async function submit(model: ImportModel) {
    model.selectedFiles.forEach(
        async file => {
            await Controllers.Images.AddImages(
                [{ Data: await toBase64(file), FileName: file.name, Tags: []}]
            );
        }
    );
}

function toBase64(file: File) {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(removeHeader(reader.result as string));
      reader.onerror = error => reject(error);
    });
}

function removeHeader(value: string) {
    return value.replace("data:", "").replace(/^.+,/, "")
}