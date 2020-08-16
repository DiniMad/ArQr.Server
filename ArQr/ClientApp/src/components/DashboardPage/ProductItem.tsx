import React, {useRef} from "react";
import {Link} from "react-router-dom";
import QrCode from "qrcode.react";

const QR_BACKGROUND_COLOR = "#19191a";
const QR_FOREGROUND_COLOR = "#dddddd";
const ITEM_SELECTED_CLASS = "selected";

type Props = {
    id: string,
    qrValue: string,
    productName: string,
    shouldHandleButton: boolean
}
const ProductItem = ({id, qrValue, productName, shouldHandleButton}: Props) => {
    const itemElement = useRef<HTMLButtonElement>(null);

    const handleItemButton = () => {
        const item = itemElement.current;
        if (!item) return;

        if (item.classList.contains(ITEM_SELECTED_CLASS))
            item.classList.remove(ITEM_SELECTED_CLASS);
        else
            item.classList.add(ITEM_SELECTED_CLASS);
    };

    return (
        <button ref={itemElement} onClick={shouldHandleButton ? handleItemButton : undefined} className="product-item">
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

export default ProductItem;