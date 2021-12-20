import { reduce, reduced, state } from 'event-reduce';
import { CounterModel } from './Counter/Counter';
import { PageManagementModel } from './PageManagement';
import { PageModel } from './PageModel';
import { SidePanelModel } from './SidePanelModel';

export class ApplicationModel {
    @reduced
    pages = reduce([new CounterModel()] as PageModel[])
        .value

    @state
    pageManagement = new PageManagementModel(this);

    @state
    sidePanel = new SidePanelModel();
}