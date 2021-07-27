import ViewModel from "react-use-controller";

import { Controllers } from './../../Framework/Controllers'

export default class TaggedImageViewModel extends ViewModel {

    files = [];

    static toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result.replace("data:", "").replace(/^.+,/, ""));
        reader.onerror = error => reject(error);
    });

    addFiles = async () => {
        this.files.forEach(
            async file => {
                await Controllers.Images.AddImages([{ data: await this.toBase64(file), fileName: file.name }])
            }
        );
        this.files = []
    }
}