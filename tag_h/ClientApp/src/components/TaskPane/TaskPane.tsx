import * as React from 'react';
import { PropsWithChildren, ReactChild } from 'react';
import { Tab, TabList, TabPanel, Tabs } from 'react-tabs';

export interface TaskPane {
    name: string;
    pane: ReactChild;
}

export const TaskPane = ({ panes }: { panes: TaskPane[] }) => {

    return (
        <div className='scrollbar'>
            <Tabs>
                <TabList>
                    {panes.map(
                        pane => <Tab key={pane.name}> {pane.name} </Tab>
                    )}
                </TabList>
                {panes.map(
                    pane => <TabPanel key={pane.name}> {pane.pane} </TabPanel>
                )}
            </Tabs>
        </div>
    );
}

export type TaskPaneContainerProps = PropsWithChildren<{
    active: boolean;
    panes: TaskPane[];
}>;

export const TaskPaneContainer = (props: TaskPaneContainerProps) => {
    let active = props.active && props.panes.length > 0;
    return (
        <div className='fill-parent'>
            <div className={'image-body center' + (active ? ' left' : '')}>
                {props.children}
            </div>
            {active && <TaskPane panes={props.panes} />}
        </div>
    );
}