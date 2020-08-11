import React from 'react';
import PropTypes from 'prop-types';
import {useHistory, useLocation} from 'react-router-dom';


import Form from './Form';
import useLogin from '../hooks/useLogin';
import {queryParameters, urls} from '../services/constants';

const LoginForm = ({onChangeFormButtonClick, onFormError}) => {
    const history = useHistory();
    const location = useLocation();

    const login = useLogin();

    const onSubmit = async (data) => {
        const success = await login(data.email, data.password);
        if (success) {
            const searchParams = new URLSearchParams(location.search);
            const redirectUrl = searchParams.get(queryParameters.returnPath) || urls.dashboardPage;
            history.replace(redirectUrl);
        }
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