import React from 'react';
import PropTypes from 'prop-types';

import Form from './Form';

const LoginForm = ({onChangeFormButtonClick, onFormError}) => {
    const onSubmit = (data) => {
        // TODO: Send Request to the server
        console.log(data);
    };

    return (
        <div id="login">
            <Form onSubmit={onSubmit}
                  onChangeFormButtonClick={onChangeFormButtonClick}
                  onFormError={onFormError}/>
        </div>
    );
};

LoginForm.propTypes = {
    onChangeFormButtonClick: PropTypes.func.isRequired,
    onFormError: PropTypes.func.isRequired,
};

export default LoginForm;