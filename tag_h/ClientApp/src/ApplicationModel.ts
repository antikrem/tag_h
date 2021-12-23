import { reduce, reduced, state } from 'event-reduce';
import { LogsModel } from './Logs/Logs';
import { CounterModel } from './Counter/Counter';
import { PageManagementModel } from './PageSystem/PageManagement';
import { PageModel } from './PageSystem/PageModel';
import { SidePanelModel } from './PageSystem/SidePanelModel';
import { ImagesModel } from './Images/Images';

export class ApplicationModel {
    @reduced
    pages = reduce([new CounterModel(), new LogsModel(), new ImagesModel()] as PageModel[])
        .value

    @state
    pageManagement = new PageManagementModel(this);

    @state
    sidePanel = new SidePanelModel();
}