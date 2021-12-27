import { reduce, reduced, state } from 'event-reduce';
import { LogsModel } from './Logs/Logs';
import { CounterModel } from './Counter/Counter';
import { PageManagementModel } from './PageSystem/PageManagement';
import { PageModel } from './PageSystem/PageModel';
import { SidePanelModel } from './PageSystem/SidePanelModel';
import { ImagesModel } from './Images/Images';
import { TagsModel } from './Tags/TagsModel';

export class ApplicationModel {
    @reduced
    tags = new TagsModel();

    @reduced
    pages = reduce([new CounterModel(), new LogsModel(), new ImagesModel(this.tags)] as PageModel[])
        .value

    @state
    pageManagement = new PageManagementModel(this);

    @state
    sidePanel = new SidePanelModel();
}