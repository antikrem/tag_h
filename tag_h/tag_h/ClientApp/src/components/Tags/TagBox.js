import React from 'react';

import { Tag } from './Tag'
import { TagAdd } from './TagAdd'


import TaggedImageViewModel from './TaggedImageViewModel'

const TagBox = (props) => {
    const { tags, deleteTag, addTag } = TaggedImageViewModel.use(props);
    
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
