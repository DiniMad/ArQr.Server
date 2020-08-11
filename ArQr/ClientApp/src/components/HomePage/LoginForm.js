import React from 'react';
import PropTypes from 'prop-types';
import {useHistory} from 'react-router-dom';


import Form from './Form';
import useLogin from '../hooks/useLogin';

const LoginForm = ({onChangeFormButtonClick, onFormError}) => {
    const history = useHistory();

    const login = useLogin();

    const onSubmit = async (data) => {
        const success = await login(data.email, data.password);
        if (success)
            history.replace('/dashboard');
        else {
            // TODO: Display login Error.
        }
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