import React from 'react';
import PropTypes from 'prop-types';

import Form from './Form';
import http from '../services/http';
import {httpStatusCode, urls} from '../services/constants';
import useLogin from '../hooks/useLogin';
import {getRedirectPath} from '../services/url';
import {useHistory} from 'react-router-dom';

const RegisterForm = ({onChangeFormButtonClick, onFormError}) => {
    const history = useHistory();

    const login=useLogin()
    
    const onSubmit = async (data) => {
        const response = await http.post(urls.registerEndPoint, {email: data.email, password: data.password});
        if(response.status===httpStatusCode.created) {
            const success = await login(data.email, data.password);
            if (success) history.replace(getRedirectPath());    
        }
        else {
            // Dont reveal ether user is exist or not
            // TODO: Display something went wrong.
        }
    };

    return (
        <div id='register'>
            <Form onSubmit={onSubmit}
                  onChangeFormButtonClick={onChangeFormButtonClick}
                  onFormError={onFormError}
                  registerMode={true}/>
        </div>
    );
};

RegisterForm.propTypes = {
    onChangeFormButtonClick: PropTypes.func.isRequired,
    onFormError: PropTypes.func.isRequired,
};

export default RegisterForm;