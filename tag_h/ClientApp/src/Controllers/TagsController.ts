// This is an auto generated test
import { Tag } from "./../Typings/Tag";

export interface TagsController {

    GetAllTags(): Promise<Tag[]>;

    CreateTag(name: string, values: string[]): Promise<void>;

    GetValues(tagId: number): Promise<string[]>;
}
