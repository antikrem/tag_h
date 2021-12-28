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
            <TagBox 
                all={tags.tags} 
                selected={model.tags} 
                add = {tag => addTag(model, tag)} 
                remove = {tag => removeTag(model, tag)} 
                render={tag => tag.name}
                comparator={(first, second) => first.id == second.id}
                search={(tag, search) => tag.name.toLowerCase().includes(search.toLowerCase())}/>
        </div>
    );
});

async function removeTag(model: ImageModel, tag: Tag) {
    await Controllers.ImageTags.RemoveTag(model.id, tag.id);
    model.events.removeTag(tag);
}

async function addTag(model: ImageModel, tag: Tag) {
    await Controllers.ImageTags.AddTag(model.id, tag.id);
    model.events.addTag(tag);
}