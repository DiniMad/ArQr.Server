const getElementOffset = (element: HTMLElement) => {
    let _x = 0;
    let _y = 0;
    while (!isNaN(element.offsetLeft) && !isNaN(element.offsetTop)) {
        _x += element.offsetLeft - element.scrollLeft;
        _y += element.offsetTop - element.scrollTop;

        if (!element.offsetParent) break;
        element = element.offsetParent as HTMLElement;
    }
    return {top: _y, left: _x};
};

export default {getElementOffset};