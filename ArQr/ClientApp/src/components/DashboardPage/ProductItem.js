import React, {useRef} from 'react';
import PropTypes from 'prop-types';
import {Link} from 'react-router-dom';
import * as QrCode from 'qrcode.react';

const QR_BACKGROUND_COLOR = '#19191a';
const QR_FOREGROUND_COLOR = '#dddddd';
const ITEM_SELECTED_CLASS = 'selected';

const ProductItem = ({id, qrValue, productName, handleButton}) => {
    const itemElement = useRef(null);

    const handleItemButton = () => {
        const item = itemElement.current;
        if (!handleButton || !item) return;

        if (item.classList.contains(ITEM_SELECTED_CLASS))
            item.classList.remove(ITEM_SELECTED_CLASS);
        else
            item.classList.add(ITEM_SELECTED_CLASS);
    };

    return (
        <button ref={itemElement} onClick={handleItemButton} className="product-item">
            <QrCode value={qrValue} bgColor={QR_BACKGROUND_COLOR} fgColor={QR_FOREGROUND_COLOR}/>
            <div className="product-overlay">
                <Link to='#'>نمایش</Link>
                <Link to='#'>ویرایش</Link>
                <Link to='#'>حذف</Link>
                <Link to='#'>ذخیره بارکد</Link>
            </div>
            <p className="product-name">{productName}</p>
        </button>
    );
};

ProductItem.propTypes = {
    id: PropTypes.string.isRequired,
    qrValue: PropTypes.string.isRequired,
    productName: PropTypes.string.isRequired,
    handleButton: PropTypes.bool.isRequired
};

export default ProductItem;