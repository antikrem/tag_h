import React, { ReactNode, useEffect, useState } from 'react';

export interface TagBoxProps<T> {
    selected: T[],
    all: T[],
    add: (tag: T) => void;
    remove: (tag: T) => void;
    render: (tag: T) => ReactNode;
    search: (tag: T, search: string) => boolean;
    comparator: (first: T, second: T) => boolean;
}

export const TagBox = <T,>(props: TagBoxProps<T>) => {
    const [selected, setSelected] = useState([] as T[]);
    const [all, setAll] = useState([] as T[]);

    const [dropped, setDropped] = useState(false);

    useEffect(() => setSelected(props.selected), [props.selected]);
    useEffect(() => setAll(props.all), [props.all]);

    return (
        <div>
            <div style={{ display: 'inline' }}>
                {selected.map((tag, index) =>
                    <Tag key={index} remove={() => props.remove(tag)}>
                        {props.render(tag)}
                    </Tag>
                )}
                <button onClick={() => setDropped(!dropped)}> |{dropped ? "x" : "+"}| </button>
            </div>
            {dropped && <TagDropDown
                options={all.filter(tag => !selected.find(t => props.comparator(t, tag)))}
                add={props.add}
                render={props.render}
                search={props.search} />}
        </div>
    );
}

interface TagProps<T> {
    remove: () => void;
    children: ReactNode;
}

const Tag = <T,>(props: TagProps<T>) => {
    return (
        <span style={{ marginRight: '5px', padding: '5px', whiteSpace: 'nowrap', backgroundColor: 'red' }}>
            <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block' }}> {props.children}</p>
            <p style={{ padding: '3px', marginBottom: '5px', display: 'inline-block', cursor: 'pointer' }} onClick={props.remove}> x </p>
        </span>
    );
}

interface TagDropDown<T> {
    options: T[],
    add: (tag: T) => void;
    render: (tag: T) => ReactNode;
    search: (tag: T, search: string) => boolean;
}

const TagDropDown = <T,>(props: TagDropDown<T>) => {
    const [search, setSearch] = useState("");

    let onKey = (event: KeyboardEvent) => {
        const key = event.key.toLowerCase();
        if ((/[a-zA-Z]/).test(key))
            setSearch(search + key);
    }

    useEffect(() => {
        document.addEventListener('keydown', onKey);
        return () => document.removeEventListener('keydown', onKey);
    }, []);

    return (
        <div>
            {props
                .options
                .filter(tag => search == "" || props.search(tag, search))
                .map((tag, index) => <div key={index} onClick={() => props.add(tag)}>
                    {props.render(tag)}
                </div>)
            }
        </div>
    );
}