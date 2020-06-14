import React from 'react';

import ProductItem from './ProductItem';

const TEST_PRODUCT_NAME = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maiores, quos!';
const TEST_QR_VALUE = 'http://facebook.github.io/react/';

const Dashboard = () => {
    return (
        <div id="dashboard">
            <div id="products">
                <ProductItem link='#' qrValue='' productName='افزودن'/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
                <ProductItem link='#' qrValue={TEST_QR_VALUE} productName={TEST_PRODUCT_NAME}/>
            </div>
        </div>
    );
};

export default Dashboard;