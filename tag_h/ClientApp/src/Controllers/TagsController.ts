// This is an auto generated test
import { Tag } from "./../Typings/Tag";

export interface TagsController {

    GetAllTags(): Promise<Tag[]>;

    CreateTag(name: string): Promise<Tag>;

    GetValues(tagId: number): Promise<string[]>;
}
