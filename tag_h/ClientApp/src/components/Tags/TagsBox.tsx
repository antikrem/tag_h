import React from "react";
import { Tag } from "../../Typings/Tag";
import { TagsInput } from "./TagsInput";

export interface TagsBoxProps {
    selected: Tag[],
    all: Tag[],
    add: (tag: Tag) => void;
    remove: (tag: Tag) => void;
}

export const TagsBox = (props: TagsBoxProps) => {
    return <TagsInput
        selected={props.selected}
        all={props.all}
        add={props.add}
        remove={props.remove}
        render={(tag: Tag) => tag.name}
        search={(tag: Tag, search: string) => tag.name.toLowerCase().includes(search.toLowerCase())}
        comparator={(first: Tag, second: Tag) => first.name == second.name} />
}