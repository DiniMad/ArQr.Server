import React, {useEffect, useState} from "react";

type Props = {
    text: string | null
};
const Notification = ({text}: Props) => {
    const [display, setDisplay] = useState(!!text);

    useEffect(() => {
        setDisplay(!!text);
    }, [text]);

    const handleCloseButton = () => setDisplay(false);

    return (
        <div id='notification' className={display ? "display" : undefined}>
            <p>{text}</p>
            <button onClick={handleCloseButton}>X</button>
        </div>
    );
};

export default Notification;