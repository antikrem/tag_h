import ViewModel from "react-use-controller";

import { Controllers } from './../../Framework/Controllers'

export default class ImportViewModel extends ViewModel {

    files = [];
    tags = [];

    static toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result.replace("data:", "").replace(/^.+,/, ""));
        reader.onerror = error => reject(error);
    });

    addFiles = (newFiles) => {
        this.files = this.files.concat(...Array.from(newFiles));
    }

    setTags = (tags) => {
        this.tags = tags;
    }

    submitFiles = async () => {
        this.files.forEach(
            async file => {
                await Controllers.Images.AddImages(
                    [{ data: await ImportViewModel.toBase64(file), fileName: file.name, tags: this.tags}]
                );
            }
        );
        this.files = [];
    }
}