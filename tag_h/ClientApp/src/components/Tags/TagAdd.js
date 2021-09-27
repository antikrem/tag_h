import React, { Component } from 'react';
import ViewModel from "react-use-controller";

import { TagDropDown } from './TagDropDown'

class TagAddViewModel extends ViewModel {

    active = false;

    componentWillUnmount() {
        this.deactivateDropDown();
    }

    activateDropDown = () => {
        //document.addEventListener("click", this.handleClickOutside);
        this.active = true;
    }

    deactivateDropDown = () => {
        //document.removeEventListener("click", this.handleClickOutside);
        this.active = false;
    }

    handleClickOutside = event => {
        const path = event.path || (event.composedPath && event.composedPath());

        if (!path.includes(this.ref)) {
            this.deactivateDropDown();
        }
    };
}

export const TagAdd = (props) => {

    const { active, activateDropDown, deactivateDropDown } = TagAddViewModel.use();

    return (
        <div>
            <button onClick={active ? deactivateDropDown : activateDropDown}> |{active ? "x" : "+"}| </button>
            {active && <TagDropDown callback={props.callback} selectedTags={props.selectedTags} />}
        </div>
    );

}