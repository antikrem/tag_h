// This is an auto generated test
import { HImage } from "./../Typings/HImage";
import { Tag } from "./../Typings/Tag";

export interface ImagesController {

    GetAll(): Promise<HImage[]>;

    GetWithTags(tags: Tag[]): Promise<HImage[]>;

    AddImages(files: SubmittedFile[]): Promise<void>;
}

export interface SubmittedFile {
    Data: string;
    FileName: string;
    Tags: Tag[];
}
