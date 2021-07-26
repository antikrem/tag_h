import React from 'react';

import { Tag } from './Tag'
import { TagAdd } from './TagAdd'


const TagBox = (props) => {
    const { tags, deleteTag, addTag } = props.viewmodel.use(props);

    // TODO: pass view model into children
    return (
        <div>
            {tags.map(tag =>
                <Tag tag={tag} callback={async () => { await deleteTag(tag) }} />
            )}
            <TagAdd callback={async (tag) => await addTag(tag)} />
        </div>
    );
}

export default TagBox;
