import React from 'react';

import { Controllers } from '../../Framework/Controllers'
import ViewModel from "react-use-controller";

import TagCreate from "./TagCreate"

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
                    <tr><td> {tag.name}</td><td> oops </td></tr>
                )}
            </table>}
        </div>
    );
}

export default TagPanel;