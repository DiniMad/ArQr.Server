import React from "react";

import ProductItem from "./ProductItem";
import AddProduct from "./AddProduct";

const PRODUCT_NAME = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maiores, quos!";
const QR_VALUE = "http://facebook.github.io/react/";

type Props = {
    isMobileSize: boolean
}
const Dashboard = ({isMobileSize}: Props) => {
    return (
        <div id="dashboard">
            <div id="products">
                <AddProduct/>
                {/* TODO: Render a message for mobile in empty product list*/}
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} shouldHandleButton={isMobileSize}/>
            </div>
        </div>
    );
};

export default Dashboard;