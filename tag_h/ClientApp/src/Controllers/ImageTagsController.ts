// This is an auto generated test

export interface ImageTagsController {

    GetTags(id: number): TagSet;

    DeleteTag(imageId: number, tagId: number): void;

    AddTag(id: number, tagId: number): void;
}

export interface TagSet {
    Empty: TagSet;
}
