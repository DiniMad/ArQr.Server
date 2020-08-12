import React from 'react';
import PropTypes from 'prop-types';
import Layout from '../Layout';

const withLayout = Component => props => (
    <Layout>
        <Component {...props} />
    </Layout>
);

withLayout.propTypes = {
    Component: PropTypes.element
};

export default withLayout;