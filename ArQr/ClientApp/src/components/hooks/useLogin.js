import {useContext} from 'react';
import http from '../../http';
import {AccessToken} from '../contexts/AccessToken';
import {httpStatusCode, oidc, urls} from '../services/constants';

const data = new FormData();
data.append(oidc.client_id.key, oidc.client_id.value);
data.append(oidc.grant_type.key, oidc.grant_type.value);
data.append(oidc.scope.key, oidc.scope.value);

const useLogin = () => {
    const [, setAccessToken] = useContext(AccessToken);

    return async (username, password) => {
        data.append('username', username);
        data.append('password', password);

        const response = await http.post(urls.loginEndPoint, data);
        if (response.status === httpStatusCode.success) {
            setAccessToken(response.data.access_token);
            localStorage.setItem(oidc.refresh_token.key, response.data.refresh_token);
            return true;
        }
        return false;
    };
};

export default useLogin;