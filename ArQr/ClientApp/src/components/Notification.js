import React, {useEffect, useState} from 'react';

const Notification = ({text}) => {
    const [display, setDisplay] = useState(!!text);

    useEffect(() => {
        setDisplay(!!text);
    }, [text]);

    const handleCloseButton = () => setDisplay(false);

    return (
        <div id='notification' className={display ? 'display' : null}>
            <p>{text}</p>
            <button onClick={handleCloseButton}>X</button>
        </div>
    );
};

Notification.propTypes = {};

export default Notification;