import React from 'react';
import PropTypes from 'prop-types';
import {Link, useLocation} from 'react-router-dom';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';

const NavBarItem = ({linkAddress, onClick, text, icon, iconSize}) => {
    const location = useLocation();
    const navClasses = location.pathname === linkAddress ? 'nav-item selected' : 'nav-item';

    return onClick ?
           <button onClick={onClick} className={navClasses}>{content()}</button> :
           <Link to={linkAddress} className={navClasses}>{content()}</Link>;

    function content() {
        return (
            <>
                <div className="text">
                    <p>{text}</p>
                </div>
                <div className="icon">
                    <FontAwesomeIcon icon={icon} size={iconSize}/>
                </div>
            </>
        );
    }
};

NavBarItem.propTypes =
    {
        linkAddress: PropTypes.string.isRequired,
        text: PropTypes.string.isRequired,
        icon: PropTypes.object.isRequired,
        iconSize: PropTypes.string.isRequired,
        onClick: PropTypes.func
    };

export default NavBarItem;