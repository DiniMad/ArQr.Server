import React from 'react';
import PropTypes from 'prop-types';
import {Link} from 'react-router-dom';
import * as QrCode from 'qrcode.react';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faPlusSquare} from '@fortawesome/free-solid-svg-icons';

const QR_BACKGROUND_COLOR = '#19191a';
const QR_FOREGROUND_COLOR = '#dddddd';

const ProductItem = ({link, qrValue, productName}) => {
    return (
        <Link to={link} className="product-item">
            {
                qrValue ?
                <QrCode value={qrValue} bgColor={QR_BACKGROUND_COLOR} fgColor={QR_FOREGROUND_COLOR}/> :
                <FontAwesomeIcon icon={faPlusSquare}/>
            }
            <p className="product-name">{productName}</p>
        </Link>
    );
};

ProductItem.propTypes = {
    link: PropTypes.string.isRequired,
    qrValue: PropTypes.string.isRequired,
    productName: PropTypes.string.isRequired
};

export default ProductItem;