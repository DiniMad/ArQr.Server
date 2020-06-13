import React, {useRef} from 'react';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faBars, faHome, faSignOutAlt, faUser} from '@fortawesome/free-solid-svg-icons';

import NavBarItem from './NavBarItem';

const NAVBAR_EXPAND_CLASS = 'expand';

const NavBar = () => {
    const navElement = useRef(null);

    const handelExpandButton = () => {
        const navBar = navElement.current;
        if (!navBar) return;

        if (navBar.classList.contains(NAVBAR_EXPAND_CLASS))
            navBar.classList.remove(NAVBAR_EXPAND_CLASS);
        else
            navBar.classList.add(NAVBAR_EXPAND_CLASS);
    };
    return (
        <nav ref={navElement}>
            <NavBarItem linkAddress='/dashboard' text='داشبورد' icon={faHome} iconSize='4x'/>
            <NavBarItem linkAddress='/dashboard' text='پروفایل' icon={faUser} iconSize='4x'/>
            <NavBarItem linkAddress='/dashboard' text='خروج' icon={faSignOutAlt} iconSize='4x'/>
            <div id="expand">
                <div id="expand-button" onClick={handelExpandButton}>
                    <FontAwesomeIcon icon={faBars} size='2x'/>
                </div>
            </div>
        </nav>
    );
};

export default NavBar;