import React, { ReactNode, useEffect, useState } from 'react';

export interface TagBoxProps<T> {
    selected: T[],
    all: T[],
    add: (tag: T) => void;
    remove: (tag: T) => void;
    render: (tag: T) => ReactNode;
}

export const TagBox = <T, >(props: TagBoxProps<T>) => {
    const [selected, setSelected] = useState([] as T[]);
    const [all, setAll] = useState([] as T[]);

    useEffect(() => setSelected(props.selected), [props.selected]);
    useEffect(() => setAll(props.all), [props.all]);

    return (
        <div style={{ display: 'inline' }}>
            {selected.map((tag, index) =>
                <Tag key={index} remove={() => props.remove(tag)}>
                    {props.render(tag)}
                </Tag>
            )}
        </div>
    );
}

interface TagProps<T> {
    remove: () => void;
    children: ReactNode;
}

const Tag = <T, >(props: TagProps<T>) => {
    return (
            <span style={{ marginRight: '5px', padding: '5px', whiteSpace: 'nowrap',  backgroundColor: 'red' }}>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block' }}> {props.children}</p>
                <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block', cursor: 'pointer' }} onClick={props.remove}> x </p>
            </span>
        );
}