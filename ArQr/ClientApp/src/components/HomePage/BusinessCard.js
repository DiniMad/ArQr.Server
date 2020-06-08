import React from 'react';
import PropTypes from 'prop-types';

const BusinessCard = ({name, cardImage, markerElement}) => {
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

BusinessCard.propTypes = {
    name: PropTypes.string.isRequired,
    cardImage: PropTypes.string.isRequired,
    markerElement: PropTypes.shape({current: PropTypes.instanceOf(Element)}).isRequired
};

export default BusinessCard;