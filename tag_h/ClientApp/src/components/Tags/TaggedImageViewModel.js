import ViewModel from "react-use-controller";

import { Controllers } from './../../Framework/Controllers'

export default class TaggedImageViewModel extends ViewModel {

    tags = [];
    image = null;

    componentDidMount() {
        this.fetchTags();
    }

    async fetchTags() {
        if (this.image) {
            this.tags = await Controllers.ImageTags.GetTags(this.image.id);
        }
        else {
            this.tags = [];
        }
    }

    deleteTag = async (tag) => {
        await Controllers.ImageTags.DeleteTag(this.image.id, tag.id);
        await this.fetchTags();
    }

    addTag = async (tag) =>
    {
        await Controllers.ImageTags.AddTag(this.image.id, tag.id);
        await this.fetchTags();
    };

    setImage = (image) => {
        if (!this.image || image.id != this.image.id) {
            this.image = image;
        }
        else {
            this.image = null;
        }
        this.fetchTags();
    }
}