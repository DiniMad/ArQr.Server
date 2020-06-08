import React from 'react';
import PropTypes from 'prop-types';

import Form from './Form';

const LoginForm = ({onChangeFormButtonClick}) => {
    const initialValues = {
        email: '',
        password: ''
    };

    const onSubmit = (data) => {
        // TODO: Send Request to the server
        console.log(data);
    };

    return (
        <Form initialValues={initialValues} 
              onSubmit={onSubmit} 
              onChangeFormButtonClick={onChangeFormButtonClick}/>
    );
};

LoginForm.propTypes = {
    onChangeFormButtonClick: PropTypes.func.isRequired,
};

export default LoginForm;