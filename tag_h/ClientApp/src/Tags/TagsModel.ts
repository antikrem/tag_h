import { asyncEvent, reduce, reduced } from "event-reduce";
import { Controllers } from "../Framework/Controllers";
import { Tag } from "../Typings/Tag";

class TagsEvents {
    fetchTags = asyncEvent<Tag[]>()
}

export class TagsModel {
    events = new TagsEvents(); 

    @reduced
    tags = reduce([] as Tag[], this.events)
        .on(e => e.fetchTags.resolved, (_, { result: tags }) => tags)
        .value;

    constructor() {
        this.events.fetchTags(Controllers.Tags.GetAllTags());
    }
}