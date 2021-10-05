import * as React from 'react'

import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';

export const ImagesTilingTaskPane = (props) => {

    return (
        <div className='image-task-pane scrollbar'>
            <Tabs>
                <TabList>
                    {props.panels.map(
                        panel => { return <Tab key={panel.name}> {panel.name} </Tab> }
                    )}
                </TabList>
                {props.panels.map(
                    panel => { return <TabPanel key={panel.name}> {panel.element} </TabPanel> }
                )}
             </Tabs>
        </div>
    );
}