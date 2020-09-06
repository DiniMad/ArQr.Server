import React, {MouseEvent, ReactChild, useRef} from "react";

import "./Modal.scss";

type Props = {
    isVisible: boolean,
    onHideModal?: () => void,
    children: ReactChild
}
const Modal = ({isVisible, onHideModal, children}: Props) => {
    let emptyAreaMouseDown = false;

    const modalElement = useRef<HTMLDivElement>(null);

    const onMouseDown = (event: MouseEvent) => {
        if (!modalElement.current || !onHideModal) return;
        emptyAreaMouseDown = event.target === modalElement.current;
    };
    const handleModalMouseUp = (event: MouseEvent) => {
        if (emptyAreaMouseDown && event.target === modalElement.current) onHideModal!();
    };
    
    if (!isVisible) return null;
    return (
        <div ref={modalElement} className="modal" onMouseDown={onMouseDown} onMouseUp={handleModalMouseUp}>
            <div className="modal__container">
                {children}
            </div>
        </div>
    );
};

export default Modal;