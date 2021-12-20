import { reduce, reduced, state } from 'event-reduce';
import { CounterModel } from './Counter/Counter';
import { PageManagementModel } from './PageSystem/PageManagement';
import { PageModel } from './PageSystem/PageModel';
import { SidePanelModel } from './PageSystem/SidePanelModel';

export class ApplicationModel {
    @reduced
    pages = reduce([new CounterModel()] as PageModel[])
        .value

    @state
    pageManagement = new PageManagementModel(this);

    @state
    sidePanel = new SidePanelModel();
}