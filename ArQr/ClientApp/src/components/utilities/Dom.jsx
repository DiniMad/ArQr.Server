const getElementOffset = element => {
    let _x = 0;
    let _y = 0;
    while (element && !isNaN(element.offsetLeft) && !isNaN(element.offsetTop)) {
        _x += element.offsetLeft - element.scrollLeft;
        _y += element.offsetTop - element.scrollTop;
        element = element.offsetParent;
    }
    return {top: _y, left: _x};
};

export default {getElementOffset}