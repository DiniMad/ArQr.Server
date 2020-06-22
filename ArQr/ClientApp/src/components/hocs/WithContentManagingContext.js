import React from 'react';

import UseProductContentManager from '../hooks/useProductContentManager';
import ProductContentTypeContext from '../contexts/ProductContentTypeContext';

const withContentManagingContext = (Product) => (props) => {
    const {data, constant, selectContent} = UseProductContentManager();

    return (
        <ProductContentTypeContext.Provider value={{data, constant, selectContent}}>
            <Product {...props}/>
        </ProductContentTypeContext.Provider>
    );
};


export default withContentManagingContext;