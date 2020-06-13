import React from 'react';
import PropTypes from 'prop-types';

import Navbar from './NavBar';

const Layout = ({children}) => {
    return (
        <main>
            <div id="content">
                {children}
            </div>
            <Navbar/>
        </main>
    );
};

Layout.propTypes = {
    children: PropTypes.oneOfType([
                                      PropTypes.arrayOf(PropTypes.element),
                                      PropTypes.element
                                  ]).isRequired
};

export default Layout;