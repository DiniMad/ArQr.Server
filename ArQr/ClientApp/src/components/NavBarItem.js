import React from 'react';
import PropTypes from 'prop-types';
import {Link} from 'react-router-dom';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';

const NavBarItem = ({linkAddress, text, icon, iconSize}) => {
    return (
        <Link to={linkAddress} className="nav-item">
            <div className="text">
                <p>{text}</p>
            </div>
            <div className="icon">
                <FontAwesomeIcon icon={icon} size={iconSize}/>
            </div>
        </Link>
    );
};

NavBarItem.propTypes =
    {
        linkAddress: PropTypes.string.isRequired,
        text: PropTypes.string.isRequired,
        icon: PropTypes.object.isRequired,
        iconSize: PropTypes.string.isRequired
    };

export default NavBarItem;