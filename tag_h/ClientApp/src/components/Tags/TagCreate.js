import React from 'react';

import { Controllers } from '../../Framework/Controllers'
import ViewModel from "react-use-controller";

class TagCreateViewModel extends ViewModel {

    name = "";
    values = [];

    constructor(refresh) {
        super();
        this.refresh = refresh;
    }

    setName = name => this.name = name;

    submit = async() => {
        await Controllers.Tags.CreateTag(this.name, []);
        this.name = "";
        this.refresh();
    }
}

const TagCreate = (props) => {
    const { name, setName, values, submit } = TagCreateViewModel.use(props.refresh);

    return (
        <div>
            <input
                type = "text"
                value = { name }
                onChange = { e => setName(e.target.value) }
            />
            
            <button onClick = { submit }> Submit </button>
        </div>
    );
}

export default TagCreate;