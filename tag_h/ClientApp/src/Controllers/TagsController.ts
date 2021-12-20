// This is an auto generated test
import { TagSet } from "./ImageTagsController";

export interface TagsController {

    GetAllTags(): TagSet;

    CreateTag(name: string, values: string[]): void;

    GetValues(tagId: number): string[];
}
