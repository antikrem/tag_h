import React from 'react';

import { Tag } from './Tag'
import { TagAdd } from './TagAdd'


const TagBox = (props) => {
    // TODO: some sort of useviewmodel which can auto use prototypes
    const { tags, deleteTag, addTag } = props.viewmodel.use(props);

    // TODO: pass view model into children
    return (
        <div>
            {tags.map((tag, i) =>
                <Tag key={i} tag={tag} callback={async () => { await deleteTag(tag) }} />
            )}
            <TagAdd callback={async (tag) => await addTag(tag)} />
        </div>
    );
}

export default TagBox;
