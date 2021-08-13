import ViewModel from "react-use-controller";

import { Controllers } from './../../Framework/Controllers'

export default class TaggedImageViewModel extends ViewModel {

    tags = [];
    image = null;

    constructor(props) {
        super();
        this.image = props.image;
    }

    componentDidMount() {
        this.fetchTags();
    }

    async fetchTags() {
        this.tags = await Controllers.ImageTags.GetTags(this.image.id);
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
}