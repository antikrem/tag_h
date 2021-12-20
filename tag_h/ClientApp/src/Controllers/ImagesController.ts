// This is an auto generated test
import { HImage } from "./../Typings/HImage";
import { Tag } from "./../Typings/Tag";

export interface ImagesController {

    GetAll(): HImage[];

    GetWithTags(tags: Tag[]): HImage[];

    AddImages(files: SubmittedFile[]): void;
}

export interface SubmittedFile {
    Data: string;
    FileName: string;
    Tags: Tag[];
}
