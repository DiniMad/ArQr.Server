import React, {useRef} from 'react';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faBars, faHome, faSignOutAlt, faUser} from '@fortawesome/free-solid-svg-icons';

import NavBarItem from './NavBarItem';

const NAVBAR_EXPAND_CLASS = 'expand';

const NavBar = () => {
    const navMenuElement = useRef(null);

    const handelExpandButton = () => {
        const navMenu = navMenuElement.current;
        if (!navMenu) return;

        if (navMenu.classList.contains(NAVBAR_EXPAND_CLASS))
            navMenu.classList.remove(NAVBAR_EXPAND_CLASS);
        else
            navMenu.classList.add(NAVBAR_EXPAND_CLASS);
    };
    return (
        <nav>
            <div id="nav-menu" ref={navMenuElement}>
                <NavBarItem linkAddress='/dashboard' text='داشبورد' icon={faHome} iconSize='4x'/>
                <NavBarItem linkAddress='/profile' text='پروفایل' icon={faUser} iconSize='4x'/>
                <NavBarItem linkAddress='/signout' text='خروج' icon={faSignOutAlt} iconSize='4x'/>
            </div>
            <div id="expand">
                <div id="expand-button" onClick={handelExpandButton}>
                    <FontAwesomeIcon icon={faBars} size='2x'/>
                </div>
            </div>
        </nav>
    );
};

export default NavBar;