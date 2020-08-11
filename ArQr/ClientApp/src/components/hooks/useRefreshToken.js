import {useContext} from 'react';
import {httpStatusCode, oidc, urls} from '../services/constants';
import http from '../../http';
import {AccessToken} from '../contexts/AccessToken';

const data = new FormData();
data.append(oidc.client_id.key, oidc.client_id.value);
data.append(oidc.grant_type.key, oidc.grant_type.refreshToken);

const useRefreshToken = () => {
    const [, setAccessToken] = useContext(AccessToken);

    return async () => {
        const refreshToken = localStorage.getItem(oidc.refresh_token.key);
        if (!refreshToken) return false;
        data.append(oidc.grant_type.refreshToken, refreshToken);
        const response = await http.post(urls.tokenEndPoint, data);
        if (response.status === httpStatusCode.success) {
            setAccessToken(response.data.access_token);
            localStorage.setItem(oidc.refresh_token.key, response.data.refresh_token);
            return true;
        }
        return false;
    };
};

export default useRefreshToken;