import React, {useState} from 'react';

import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';
import Introduction from './Introduction.Mobile';
import useHomePageNavigation from '../hooks/useHomePageNavigation.Mobile';
import Notification from '../Notification';
import useSilentLogin from '../hooks/useSilentLogin';

const HomeMobile = React.memo(() => {
    const [errorText, setErrorText] = useState(null);

    useSilentLogin();

    const {
        data: {introductionClasses, containerClasses, isItLoginPage, isItRegisterPage},
        actionTypes: [LoginPage, RegisterPage],
        navigateTo
    } = useHomePageNavigation();

    const setError = (condition, error) => condition && setErrorText(error);

    const onLoginFormError = error => setError(isItLoginPage, error);
    const onRegisterFormError = error => setError(isItRegisterPage, error);
    const goToLoginPage = () => navigateTo(LoginPage);
    const goToRegisterPage = () => navigateTo(RegisterPage);

    return (
        <div id='home'>
            <div id='header'>
                <h1>ArQr</h1>
            </div>
            <div id="container" className={containerClasses}>
                <RegisterForm onChangeFormButtonClick={goToLoginPage} onFormError={onRegisterFormError}/>
                <Introduction className={introductionClasses}
                              handleLoginButton={goToLoginPage}
                              handleRegisterButton={goToRegisterPage}/>
                <LoginForm onChangeFormButtonClick={goToRegisterPage} onFormError={onLoginFormError}/>
            </div>
            <Notification text={errorText}/>
        </div>
    );
});

export default HomeMobile;