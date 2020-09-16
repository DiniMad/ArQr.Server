import React, {MouseEventHandler} from "react";
import {useHistory} from "react-router-dom";
import {useToasts} from "react-toast-notifications";


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
    const {addToast}=useToasts()

    const onSubmit = async (data: UserIdentity) => {
        const success = await login(data.email, data.password);
        if (success) history.replace(getRedirectPath());
        else {
            addToast("نام کاربری یا رمز عبور اشتباه است.",{appearance:"error"})
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