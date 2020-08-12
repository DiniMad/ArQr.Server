import {useContext} from 'react';
import * as jwtDecode from 'jwt-decode';

import {oidc} from '../services/constants';
import {AccessTokenContext} from '../contexts/AccessTokenContext';
import {UserContext} from '../contexts/UserContext';

const useIdentityStore = () => {
    const [, setAccessToken] = useContext(AccessTokenContext);
    const [, setUser] = useContext(UserContext);

    return (accessToken,refreshToken) => {
        const tokenData = jwtDecode(accessToken);
        const user = {
            id: tokenData.sub,
            username: tokenData.username,
            email: tokenData.email,
            emailConfirmed: tokenData.emailConfirmed === 'True',
            phoneNumber: tokenData.phoneNumber,
            phoneNumberConfirmed: tokenData.phoneNumberConfirmed === 'True',
        };
        setUser(user);
        setAccessToken(accessToken);
        localStorage.setItem(oidc.refresh_token.key, refreshToken);
    };
};

export default useIdentityStore;