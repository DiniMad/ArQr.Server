import useIdentityStore from './useIdentityStore';
import http from '../services/http';
import {httpStatusCode, oidc, urls} from '../services/constants';

const data = new FormData();
data.append(oidc.client_id.key, oidc.client_id.value);
data.append(oidc.grant_type.key, oidc.grant_type.refreshToken);

const useRefreshToken = () => {
    const storeIdentities=useIdentityStore();

    return async () => {
        const refreshToken = localStorage.getItem(oidc.refresh_token.key);
        if (!refreshToken) return false;
        
        data.append(oidc.grant_type.refreshToken, refreshToken);
        const response = await http.post(urls.tokenEndPoint, data);
        if (response.status === httpStatusCode.success) {
            storeIdentities(response.data.access_token,response.data.refresh_token)
            return true;
        }
        return false;
    };
};

export default useRefreshToken;