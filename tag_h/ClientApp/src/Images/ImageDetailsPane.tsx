import { reactive } from 'event-reduce-react';
import React from 'react';
import { TagBox } from '../components/Tags/TagBox';
import { ImageModel } from './Images';
import { Tag } from './../Typings/Tag'
import { Controllers } from '../Framework/Controllers';
import { TagsModel } from '../Tags/TagsModel';

export const ImageDetailsPane = reactive(function Images({ model, tags }: { model: ImageModel, tags: TagsModel }) {
    return (
        <div>
            <TagBox all={tags.tags} selected={model.tags} add = {tag=>{}} remove = {tag => removeTag(model, tag as Tag)} />
        </div>
    );
});

async function removeTag(model: ImageModel, tag: Tag) {
    await Controllers.ImageTags.RemoveTag(model.id, tag.id);
    model.events.removeTag(tag);
}
