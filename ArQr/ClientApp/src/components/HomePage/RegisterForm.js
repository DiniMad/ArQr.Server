import React from 'react';
import PropTypes from 'prop-types';

import Form from './Form';

const RegisterForm = ({onChangeFormButtonClick}) => {
    const initialValues = {
        email: '',
        password: '',
        passwordConfirmation:''
    };

    const onSubmit = (data) => {
        // TODO: Send Request to the server
        console.log(data);
    };

    return (
        <Form initialValues={initialValues}
              onSubmit={onSubmit}
              onChangeFormButtonClick={onChangeFormButtonClick}
              registerMode={true}/>
    );
};

RegisterForm.propTypes = {
    onChangeFormButtonClick: PropTypes.func.isRequired,
};

export default RegisterForm;