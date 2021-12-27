import { reactive } from 'event-reduce-react';
import React from 'react';

export interface Taggable {
    name: string;
}

export interface TagBoxProps {
    selected: Taggable[],
    all: Taggable[],
    add: (tag: Taggable) => void;
    remove: (tag: Taggable) => void;
}

export const TagBox = reactive((props: TagBoxProps) => {

    return (
        <div style={{ display: 'inline' }}>
            {props.selected.map((tag, index) =>
                <Tag key={index} tag={tag} remove={() => props.remove(tag)}/>
            )}
        </div>
    );
});

const Tag = ({ tag, remove }: {tag: Taggable, remove: () => void}) => {
    return (
            <span style={{ marginRight: '5px', padding: '5px', whiteSpace: 'nowrap',  backgroundColor: 'red' }}>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block' }}> {tag.name}</p>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block', cursor: 'pointer' }} onClick={remove}> x </p>
            </span>
        );
}

