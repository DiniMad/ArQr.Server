import React, {MouseEventHandler} from "react";
import {Link, useLocation} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {IconProp, SizeProp} from "@fortawesome/fontawesome-svg-core";

type Props = {
    handleClick: string | MouseEventHandler,
    text: string,
    icon: IconProp,
    iconSize: SizeProp
}
const NavBarItem = ({handleClick, text, icon, iconSize}: Props) => {
    const location = useLocation();
    const navClasses = location.pathname === handleClick ? "nav-item selected" : "nav-item";

    return typeof handleClick === "string" ?
           <Link to={handleClick} className={navClasses}>{content()}</Link> :
           <button onClick={handleClick} className={navClasses}>{content()}</button>;

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

export default NavBarItem;