import React, {MouseEventHandler} from "react";
import {useHistory} from "react-router-dom";


import Form from "./Form";
import useLogin from "../hooks/useLogin";
import {getRedirectPath} from "../services/url";
import {UserIdentity} from "../types";

type Props = {
    onFormError: (error: string | null) => void,
    onChangeFormButtonClick: MouseEventHandler,
}
const LoginForm = ({onChangeFormButtonClick, onFormError}: Props) => {
    const history = useHistory();

    const login = useLogin();

    const onSubmit = async (data: UserIdentity) => {
        const success = await login(data.email, data.password);
        if (success) history.replace(getRedirectPath());
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

export default LoginForm;