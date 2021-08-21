import React from 'react';

import { Controllers } from '../../Framework/Controllers'
import ViewModel from "react-use-controller";

import TagCreate from "./TagCreate"

class TagEntryViewModel extends ViewModel {

    values = [];

    constructor(tag) {
        super();
        this.tag = tag;
        console.log(tag)
    }

    componentDidMount() {
        this.loadTag();
    }

    loadTag = async () => this.values = await Controllers.Tags.GetValues(this.tag.id);
}

const TagEntry = (props) => {
    const { values } = TagEntryViewModel.use(props.tag);

    return <tr><td> {props.tag.name}</td><td> {values.join(", ")} </td></tr>;
}

class TagPanelViewModel extends ViewModel {

    tags = [];

    componentDidMount() {
        this.refreshTags();
    }

    refreshTags = async () => this.tags = await Controllers.Tags.GetAllTags();
}

const TagPanel = () => {
    const { tags, refreshTags } = TagPanelViewModel.use();

    return (
        <div>
            <h1>Tags:</h1>
            <TagCreate refresh = { refreshTags }/>
            {<table>
                <tr>
                    <th>Tag</th>
                    <th>Catergory</th>
                </tr>
                {tags.map(tag => 
                    <TagEntry tag={tag} />
                )}
            </table>}
        </div>
    );
}

export default TagPanel;