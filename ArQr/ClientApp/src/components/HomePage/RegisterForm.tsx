import React, {MouseEventHandler} from "react";
import {useHistory} from "react-router-dom";

import Form from "./Form";
import http from "../services/http";
import {httpStatusCode, urls} from "../services/constants";
import useLogin from "../hooks/useLogin";
import {getRedirectPath} from "../services/url";
import {UserIdentity} from "../types";

type Props = {
    onFormError: (error: string | null) => void,
    onChangeFormButtonClick: MouseEventHandler,
}
const RegisterForm = ({onChangeFormButtonClick, onFormError}: Props) => {
    const history = useHistory();

    const login = useLogin();

    const onSubmit = async (data: UserIdentity) => {
        const response = await http.post(urls.registerEndPoint, data);
        if (response.status === httpStatusCode.created) {
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

export default RegisterForm;