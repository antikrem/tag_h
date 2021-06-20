import React, { Component } from 'react';
import { Nav, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return null;//(
            //<NavSide
            //        onSelect={(selectedKey) => alert(`selected ${selectedKey}`)}
            //    className="flex-column navbar"
            //>
            //        <NavSideItem/>
            //        <NavSideItem/>
            //        <NavItem>
            //            <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
            //        </NavItem>
            //        <NavItem>
            //            <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
            //        </NavItem>
            //        <NavItem>
            //            <NavLink tag={Link} className="text-dark" to="/images-view">Images View</NavLink>
            //        </NavItem>
            //</NavSide>
            //);
    }
}