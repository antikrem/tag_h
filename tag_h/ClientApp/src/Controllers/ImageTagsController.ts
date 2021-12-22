// This is an auto generated test

export interface ImageTagsController {

    GetTags(id: number): Promise<TagSet>;

    DeleteTag(imageId: number, tagId: number): Promise<void>;

    AddTag(id: number, tagId: number): Promise<void>;
}

export interface TagSet {
    Empty: TagSet;
}
