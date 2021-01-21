window.qrcodeSvg = async (text) => {
    try {
        const qrcode = await QRCode.toString(text);
        return qrcode;
    } catch (err) {
        console.error(err)
    }
};