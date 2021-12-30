import React, { ReactNode, useEffect, useState } from 'react';

export interface TagsInputProps<T> {
    selected: T[],
    all: T[],
    add: (tag: T) => void;
    remove: (tag: T) => void;
    render: (tag: T) => ReactNode;
    search: (tag: T, search: string) => boolean;
    create?: (input: string) => T | Promise<T>;
    comparator: (first: T, second: T) => boolean;
}

export const TagsInput = <T,>(props: TagsInputProps<T>) => {
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
                search={props.search}
                create={props.create} />}
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
    create?: (input: string) => T | Promise<T>;
}

const TagDropDown = <T,>(props: TagDropDown<T>) => {
    const [search, setSearch] = useState("");

    return (
        <div>
            <input autoFocus 
                type="text" 
                value={search} 
                onChange={(input) => setSearch(input.target.value)} 
                onKeyPress={(event) => submitOrCreate(event.key)} />
            {props
                .options
                .filter(tag => search == "" || props.search(tag, search))
                .map((tag, index) => <div key={index} onClick={() => props.add(tag)}>
                    {props.render(tag)}
                </div>)
            }
        </div>
    );

    function submitOrCreate(key: string) {
        if (key != "Enter") {
            return;
        }

        let options = props.options
            .filter(tag => search == "" || props.search(tag, search));

        if (options.length == 1) {
            props.add(options[0]);
            setSearch("");

        }
        else if (search.trim() != "" && props.create) {
            var thing = props.create(search);
            Promise.resolve(thing)
                .then(props.add); 
            setSearch("");
        }
    }
}