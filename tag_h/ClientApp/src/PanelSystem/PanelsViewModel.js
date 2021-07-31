import ViewModel from "react-use-controller";
import React, { Component } from 'react';

import { Home } from './../components/Home';
import ImagesView from './../components/Images/ImagesView';
import { LogView } from './../components/Logs/LogView';
import Counter from './../components/Counter';
import ImportView from './../components/Import/ImportView'

export default class PanelViewModel extends ViewModel {

    firmPanels = [
        ["Home", <Home />],
        ["Counter", <Counter />],
        ["Images", <ImagesView />],
        ["Import", <ImportView />],
        ["Log", <LogView />]
    ];

    activePane = this.firmPanels[0][1];

    setActivePane = (pane) => {
        this.activePane = pane;
    }

    getSidebarProps = () => {
        return this.firmPanels.map(
            (panel, i) => { return { text: panel[0], onClick: () => this.setActivePane(panel[1]) } }
        );
    }

}