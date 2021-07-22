import { Controllers } from './../../Framework/Controllers'

export class TaggedImageViewModel {

    constructor(image) {
        this.image = image;
    }

    async getTags() {
        return await Controllers.ImageTags.GetTags(this.image.uuid);
    }

    async deleteTag(tag) {
        await Controllers.ImageTags.DeleteTag(this.image.uuid, tag.value);
    }

    async addTag(tag) {
        await Controllers.ImageTags.AddTag(this.image.uuid, tag.value);
    }

}