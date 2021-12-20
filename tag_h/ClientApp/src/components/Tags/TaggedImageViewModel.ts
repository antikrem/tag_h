import ViewModel from "react-use-controller";

import { HImage } from "../../Typings/HImage"
import { Tag } from "../../Typings/Tag"

import { Controllers } from './../../Framework/Controllers'

export default class TaggedImageViewModel extends ViewModel {

    tags : Tag[] = [];
    image : HImage | null = null;

    componentDidMount() {
        this.fetchTags();
    }

    async fetchTags() {
        if (this.image) {
            this.tags = await Controllers.ImageTags.GetTags(this.image.id) as unknown as Tag[];
        }
        else {
            this.tags = [];
        }
    }

    deleteTag = async (tag : Tag) => {
        await Controllers.ImageTags.DeleteTag(this.image!.id, tag.id);
        await this.fetchTags();
    }

    addTag = async (tag : Tag) =>
    {
        await Controllers.ImageTags.AddTag(this.image!.id, tag.id);
        await this.fetchTags();
    };

    setImage = (image : HImage) => {
        if (!this.image || image.id != this.image.id) {
            this.image = image;
        }
        else {
            this.image = null;
        }
        this.fetchTags();
    }
}