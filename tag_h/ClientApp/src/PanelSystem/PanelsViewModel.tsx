import ViewModel from "react-use-controller";
import * as React  from 'react';

import { Home } from '../components/Home';
import ImagesView from '../components/Images/ImagesView';
import TagPanel from '../components/Tags/TagPanel';
import { LogPanel } from '../components/Logs/LogPanel';
import { Counter, CounterModel } from '../components/Counter';
import ImportView from '../components/Import/ImportView'

class Panel {
    constructor(
        public name: string,
        public component: JSX.Element,
        public viewmodel: ViewModel | null
        ) { }
}

export class PanelViewModel extends ViewModel {

    firmPanels = [
        new Panel("Home", <Home />, null),
        new Panel("Counter", <Counter model={new CounterModel()}/>, null),
        new Panel("Images", <ImagesView />, null),
        new Panel("Tags", <TagPanel />, null),
        new Panel("Import", <ImportView />, null),
        new Panel("Log", <LogPanel />, null)
    ];

    activePane = this.firmPanels[0].component;

    setActivePane = (pane: JSX.Element) => {
        this.activePane = pane;
    }

    getSidebarProps = () => {
        return this.firmPanels.map(
            (panel, _) => { return { text: panel.name, onClick: () => this.setActivePane(panel.component) } }
        );
    }

}