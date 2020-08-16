import React, {RefObject} from "react";

type Props={
    name:string,
    cardImage:string,
    markerElement:RefObject<HTMLDivElement>
}
const BusinessCard = ({name, cardImage, markerElement}:Props) => {
    return (
        <div id={`rotary-template-${name}`} className="rotary-template-container">
            <div className="card-template">
                <img className="card-template-img" src={cardImage} alt="Business card"/>
                <div ref={markerElement}
                     id={`marker-card-${name}`}
                     className="card-template-marker"/>
            </div>
        </div>
    );
};

export default BusinessCard;