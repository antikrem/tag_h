import React from 'react'
import ViewModel from "react-use-controller";

export default class PanelViewModel extends ViewModel {

    active = true;
    panels = [];

    constructor(panels) {
        this.panels = panels;
    }

    getPanels = () => {
        return this.panels;
    }

    setActive = (active) => {
        this.active = active;
    }
}


const ImagesTilingTaskPane = (props) => {

    return (
        <div className='image-task-pane scrollbar'>
            <Tabs>
                {props.panels.map(
                    panel => {
                        <Tab event={panel.name} title={panel.name}>
                            {panel.element}
                        </Tab>
                    }
                 )}
             </Tabs>
        </div>
    );
}