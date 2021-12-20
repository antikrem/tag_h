import { reduce, event, reduced } from 'event-reduce';
import { PageModel } from './PageModel';
import { ApplicationModel } from './ApplicationModel';


export class PageManagementEvents {
    selectPage = event<PageModel>();
}
export class PageManagementModel {
    events = new PageManagementEvents();

    @reduced
    activePage = reduce(this.model.pages[0], this.events)
        .on(e => e.selectPage, (_, page) => page)
        .value;

    constructor(private model: ApplicationModel) { }
}
