import React from 'react';
import PropTypes from 'prop-types';

import Form from './Form';

const RegisterForm = ({onChangeFormButtonClick,onFormError}) => {
    const onSubmit = (data) => {
        // TODO: Send Request to the server
        console.log(data);
    };

    return (
        <Form onSubmit={onSubmit}
              onChangeFormButtonClick={onChangeFormButtonClick}
              onFormError={onFormError}
              registerMode={true}/>
    );
};

RegisterForm.propTypes = {
    onChangeFormButtonClick: PropTypes.func.isRequired,
    onFormError: PropTypes.func.isRequired,
};

export default RegisterForm;