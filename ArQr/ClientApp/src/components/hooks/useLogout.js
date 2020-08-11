import {useContext} from 'react';
import {useHistory} from 'react-router-dom';
import {AccessToken} from '../contexts/AccessToken';
import {oidc, urls} from '../services/constants';

const useLogout = () => {
    const history = useHistory();

    const [, setAccessToken] = useContext(AccessToken);

    return () => {
        localStorage.removeItem(oidc.refresh_token.key);
        setAccessToken(null);
        history.push(urls.homePage);
    };
};

export default useLogout;