import React from 'react';

import ProductItem from './ProductItem';
import AddProduct from './AddProduct';

const PRODUCT_NAME = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maiores, quos!';
const QR_VALUE = 'http://facebook.github.io/react/';

const Dashboard = ({handleButton}) => {
    return (
        <div id="dashboard">
            <div id="products">
                <AddProduct/>
                {/*Render a message for mobile in empty product list*/}
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
                <ProductItem id='#' qrValue={QR_VALUE} productName={PRODUCT_NAME} handleButton={handleButton}/>
            </div>
        </div>
    );
};

export default Dashboard;