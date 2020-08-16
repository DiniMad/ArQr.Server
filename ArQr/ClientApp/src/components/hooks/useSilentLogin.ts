import {useEffect} from 'react';
import {useHistory} from 'react-router-dom';

import {getRedirectPath} from '../services/url';
import useRefreshToken from './useRefreshToken';

const useSilentLogin = () => {
    const history = useHistory();

    const refreshToken = useRefreshToken();

    useEffect(() => {
        refreshToken().then(authenticated => authenticated && history.replace(getRedirectPath()));
    }, []);
};

export default useSilentLogin;