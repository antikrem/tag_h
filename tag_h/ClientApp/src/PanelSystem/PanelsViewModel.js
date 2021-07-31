import ViewModel from "react-use-controller";
import React, { Component } from 'react';

import { Home } from './../components/Home';
import ImagesView from './../components/Images/ImagesView';
import { LogView } from './../components/Logs/LogView';
import Counter from './../components/Counter';
import ImportView from './../components/Import/ImportView'

class Panel {
    constructor(name, component, viewmodel) {
        this.name = name;
        this.component = component;
        this.viewmodel = viewmodel;
    }
}

export default class PanelViewModel extends ViewModel {

    firmPanels = [
        new Panel("Home", <Home />, null),
        new Panel("Counter", <Counter />, null),
        new Panel("Images", <ImagesView />, null),
        new Panel("Import", <ImportView />, null),
        new Panel("Log", <LogView />, null)
    ];

    activePane = this.firmPanels[0].component;

    setActivePane = (pane) => {
        this.activePane = pane;
    }

    getSidebarProps = () => {
        return this.firmPanels.map(
            (panel, _) => { return { text: panel.name, onClick: () => this.setActivePane(panel.component) } }
        );
    }

}