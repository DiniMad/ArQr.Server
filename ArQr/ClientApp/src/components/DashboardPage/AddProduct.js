import React from 'react';
import {Link} from 'react-router-dom';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faPlusSquare} from '@fortawesome/free-solid-svg-icons';

const AddProduct = () => {
    return (
        <Link to='/product' className="add-product">
            <FontAwesomeIcon icon={faPlusSquare}/>
            <p className="product-name">افزودن</p>
        </Link>
    );
};

export default AddProduct;