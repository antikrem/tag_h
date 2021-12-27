// This is an auto generated test
import { ImageViewModel } from "./../Typings/ImageViewModel";
import { Tag } from "./../Typings/Tag";

export interface ImagesController {

    GetAll(): Promise<ImageViewModel[]>;

    AddImages(files: SubmittedFile[]): Promise<void>;
}

export interface SubmittedFile {
    Data: string;
    FileName: string;
    Tags: Tag[];
}
