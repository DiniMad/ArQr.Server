import React, {useState} from "react";

import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import Introduction from "./Introduction.Mobile";
import useHomePageNavigation from "../hooks/useHomePageNavigation.Mobile";
import Notification from "../Notification";
import useSilentLogin from "../hooks/useSilentLogin";
import {UseState} from "../types";

const HomeMobile = React.memo(() => {
    const [errorText, setErrorText] = useState<UseState<string>>(null);

    useSilentLogin();

    const {
        data       : {introductionClasses, containerClasses, isItLoginPage, isItRegisterPage},
        actionTypes: [LoginPage, RegisterPage],
        navigateTo
    } = useHomePageNavigation();

    const setError = (condition: boolean, error: string | null) => condition && setErrorText(error);

    const onLoginFormError = (error: string | null) => setError(isItLoginPage, error);
    const onRegisterFormError = (error: string | null) => setError(isItRegisterPage, error);
    const goToLoginPage = () => navigateTo(LoginPage);
    const goToRegisterPage = () => navigateTo(RegisterPage);

    return (
        <div id='home'>
            <div id='header'>
                <h1>ArQr</h1>
            </div>
            <div id="container" className={containerClasses || undefined}>
                <RegisterForm onChangeFormButtonClick={goToLoginPage} onFormError={onRegisterFormError}/>
                <Introduction className={introductionClasses || undefined}
                              handleLoginButton={goToLoginPage}
                              handleRegisterButton={goToRegisterPage}/>
                <LoginForm onChangeFormButtonClick={goToRegisterPage} onFormError={onLoginFormError}/>
            </div>
            <Notification text={errorText}/>
        </div>
    );
});

export default HomeMobile;