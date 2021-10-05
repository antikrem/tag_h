import * as React from 'react'
import './Sidebar.css';

export const SidebarItem = (props: { onClick: () => void, text: string}) => {
    return (
        <div className="sidebar-item">
            <button onClick={props.onClick}><p>{props.text}</p></button>
        </div>
    );
}