import React from 'react';

import { useViewModel } from '../../Framework/UseViewModel'

import { Tag } from './Tag'
import { TagAdd } from './TagAdd'


const TagBox = (props) => {
    const { tags, deleteTag, addTag } = useViewModel(props.viewmodel, props);

    return (
        <div style={{ display: 'inline' }}>
            {tags.map((tag) =>
                <Tag key={tag.name} tag={tag} callback={async () => { await deleteTag(tag) }} />
            )}
            <TagAdd callback={async (tag) => await addTag(tag)} selectedTags={tags} />
        </div>
    );
}

export default TagBox;
