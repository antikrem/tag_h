// This is an auto generated test
import { FileViewModel } from "./../Typings/FileViewModel";
import { Tag } from "./../Typings/Tag";

export interface FilesController {

    GetAll(): Promise<FileViewModel[]>;

    AddFiles(files: SubmittedFile[]): Promise<void>;
}

export interface SubmittedFile {
    Data: string;
    FileName: string;
    Tags: Tag[];
}
