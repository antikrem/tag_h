import ViewModel from "react-use-controller";

export default class ImportTagViewModel extends ViewModel {

    tags = [];

    constructor(props) {
        super();
        this.setTags = props.setTags;
    }

    deleteTag = (tag) => {
        this.tags = this.tags.filter(item => item != tag);
        this.setTags(this.tags);
    }

    addTag = (tag) => {
        this.tags = this.tags.concat(tag);
        this.setTags(this.tags);
    };
}