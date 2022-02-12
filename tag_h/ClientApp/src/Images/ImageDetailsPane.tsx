import { reactive } from 'event-reduce-react';
import React from 'react';
import { TagsBox } from '../components/Tags/TagsBox';
import { Controllers } from '../Framework/Controllers';
import { TagsModel } from '../Tags/TagsModel';
import { Tag } from './../Typings/Tag';
import { ImageModel } from './Images';

export const ImageDetailsPane = reactive(function Images({ model, tags }: { model: ImageModel, tags: TagsModel }) {
    return <TagsBox
        all={tags.tags}
        selected={model.tags}
        add={tag => addTag(model, tag)}
        remove={tag => removeTag(model, tag)} />
});

async function removeTag(model: ImageModel, tag: Tag) {
    await Controllers.FileTags.RemoveTag(model.id, tag.id);
    model.events.removeTag(tag);
}

async function addTag(model: ImageModel, tag: Tag) {
    await Controllers.FileTags.AddTag(model.id, tag.id);
    model.events.addTag(tag);
}