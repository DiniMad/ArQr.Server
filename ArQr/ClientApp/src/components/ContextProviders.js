import React from 'react';
import PropTypes from 'prop-types';
import AccessTokenProvider from './contexts/AccessTokenContext';
import UserProvider from './contexts/UserContext';

const ContextProviders = ({children}) => {
    return (
        <AccessTokenProvider>
            <UserProvider>
                {children}
            </UserProvider>
        </AccessTokenProvider>
    );
};

ContextProviders.propTypes = {
    children: PropTypes.oneOfType([PropTypes.element,
                                      PropTypes.arrayOf(PropTypes.element)]).isRequired
};

export default ContextProviders;