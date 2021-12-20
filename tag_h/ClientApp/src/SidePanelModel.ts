import { event, reduce, reduced } from 'event-reduce';

export class SidePanelModel {
    toggleCollapse = event();

    @reduced
    collapsed = reduce(false)
        .on(this.toggleCollapse, value => !value)
        .value;
}
