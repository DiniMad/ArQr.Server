import {useContext} from 'react';
import * as jwtDecode from 'jwt-decode';

import http from '../services/http';
import {AccessTokenContext} from '../contexts/AccessTokenContext';
import {httpStatusCode, oidc, urls} from '../services/constants';
import {UserContext} from '../contexts/UserContext';

const data = new FormData();
data.append(oidc.client_id.key, oidc.client_id.value);
data.append(oidc.grant_type.key, oidc.grant_type.password);
data.append(oidc.scope.key, oidc.scope.value);

const useLogin = () => {
    const [, setAccessToken] = useContext(AccessTokenContext);
    const [, setUser] = useContext(UserContext);

    return async (username, password) => {
        data.append('username', username);
        data.append('password', password);

        const response = await http.post(urls.tokenEndPoint, data);
        if (response.status === httpStatusCode.success) {
            setAccessToken(response.data.access_token);
            localStorage.setItem(oidc.refresh_token.key, response.data.refresh_token);
            
            const tokenData = jwtDecode(response.data.access_token);
            const user = {
                id: tokenData.sub,
                username: tokenData.username,
                email: tokenData.email,
                emailConfirmed: tokenData.emailConfirmed === 'True',
                phoneNumber: tokenData.phoneNumber,
                phoneNumberConfirmed: tokenData.phoneNumberConfirmed === 'True',
            };
            setUser(user);
            return true;
        }
        return false;
    };
};

export default useLogin;