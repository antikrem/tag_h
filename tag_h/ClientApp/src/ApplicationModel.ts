import { reduce, reduced, state } from 'event-reduce';
import { LogsModel } from './Logs/Logs';
import { CounterModel } from './Counter/Counter';
import { PageManagementModel } from './PageSystem/PageManagement';
import { PageModel } from './PageSystem/PageModel';
import { SidePanelModel } from './PageSystem/SidePanelModel';
import { ImagesModel } from './Images/Images';
import { TagsModel } from './Tags/TagsModel';
import { ImportModel } from './Import/Import';

export class ApplicationModel {
    @state
    tags = new TagsModel();

    @reduced
    pages = reduce([new CounterModel(), new LogsModel(), new ImagesModel(this.tags), new ImportModel()] as PageModel[])
        .value

    @state
    pageManagement = new PageManagementModel(this);

    @state
    sidePanel = new SidePanelModel();
}