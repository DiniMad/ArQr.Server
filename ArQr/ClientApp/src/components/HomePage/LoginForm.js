import React from 'react';
import PropTypes from 'prop-types';
import {useHistory} from 'react-router-dom';


import Form from './Form';

const LoginForm = ({onChangeFormButtonClick, onFormError}) => {
    const history = useHistory();

    const onSubmit = (data) => {
        // TODO: Send Request to the server
        console.log(data);
        history.replace('/dashboard');
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