// This is an auto generated test
import { TagSet } from "./ImageTagsController";

export interface TagsController {

    GetAllTags(): Promise<TagSet>;

    CreateTag(name: string, values: string[]): Promise<void>;

    GetValues(tagId: number): Promise<string[]>;
}
